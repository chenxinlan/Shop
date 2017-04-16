var myApp = angular.module('myApp', ['ui.router', 'ct.ui.router.extras', 'ngCookies',
     'ui.grid', 'ui.grid.pagination', 'ui.grid.selection', 'ui.grid.edit',
    'commodityTypeModule','stockModule'
   ],
    function ($httpProvider) {
        // Use x-www-form-urlencoded Content-Type
        $httpProvider.defaults.headers.put['Content-Type'] = 'application/x-www-form-urlencoded';
        $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
        /**
         * The workhorse; converts an object to x-www-form-urlencoded serialization.
         * @param {Object} obj
         * @return {String}
         */
        var param = function (obj) {
            var query = '', name, value, fullSubName, subName, subValue, innerObj, i;

            for (name in obj) {
                value = obj[name];
                if (value instanceof Object) {
                    query += encodeURIComponent(name) + '=' + encodeURIComponent(angular.toJson(value)) + '&';
                } else if (value !== undefined && value !== null) {
                    query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
                }
            }
            return query.length ? query.substr(0, query.length - 1) : query;
        };
        // Override $http service's default transformRequest
        $httpProvider.defaults.transformRequest = [function (data) {
            return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
        }];
    });


//动态添加js,css;css有效,js需要动态注入Module，未完成。例子http://www.tuicool.com/articles/3Mbi2y6
var DynamicLoading = {
    css: function (path) {
        if (!path || path.length === 0) {
            throw new Error('argument "path" is required !');
        }
        var head = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.href = path;
        link.rel = 'stylesheet';
        link.type = 'text/css';
        head.appendChild(link);
    },
    js: function (path) {
        if (!path || path.length === 0) {
            throw new Error('argument "path" is required !');
        }
        var head = document.getElementsByTagName('head')[0];
        var script = document.createElement('script');
        script.src = path;
        script.type = 'text/javascript';
        head.appendChild(script);
    }
}

/**
 * 由于整个应用都会和路由打交道，所以这里把$state和$stateParams这两个对象放到$rootScope上，方便其它地方引用和注入。
 * 这里的run方法只会在angular启动的时候运行一次。
 */
myApp.run(function ($rootScope, $state, $stateParams, $http, $cookies, $templateCache) {
    $rootScope.$state = $state;
    $rootScope.$stateParams = $stateParams;
    $rootScope._ = window._;// use in views, ng-repeat="x in _.range(3)"
});

//lodash://可以在ng-repeat等标签上使用
myApp.constant('_', window._);

//页面的提示信息(常量是没法被装饰器拦截的)
myApp.constant('COMMON_CONSTANT', {
    SERVERFAIL: '服务器错误,保存失败！',
    NOTFOUND: '无法查询到该数据！',
    FAILEDLOADING: '加载失败！'
});

//缓存模板
myApp.run([
    '$templateCache', function ($templateCache) {
        $templateCache.put('appTemp/DefaultDialog',
            '<div class="ngdialog-message">' +
            '<h4 style="border-bottom:1px solid #ccc;padding-bottom:5px;">{{title}}</h4>' +
            '<div>{{msgStr}}</div>' +
            '</div>' +
            '<div class="ngdialog-buttons mt">' +
            '<button type="button" class="ngdialog-button btn btn-primary btn-sm" ng-click="closeThisDialog()">关闭</button>' +
            '</div>');


        $templateCache.put('appTemp/ConfirmDialog',
            '<div class="ngdialog-message">' +
            '<h4 style="border-bottom:1px solid #ccc;padding-bottom:5px;">{{title}}</h4>' +
            '<div>{{msgStr}}</div>' +
            '</div>' +
            '<div class="ngdialog-buttons mt">' +
            '<button type="button" class="ngdialog-button btn btn-danger btn-xs" ng-click="closeThisDialog()">关闭</button>' +
            ' <button type="button" class="ngdialog-button btn btn-primary btn-sm" ng-click="confirm()">确定</button>' +
            '</div>');
    }
]);

myApp.factory('httpInterceptor', ['$injector', '$location', '$q', '$cookies', function ($injector, $location, $q, $cookies) {

    var httpInterceptor = {
        'request': function (config) {
            //以后方便做令牌处理.(每个请求都加请求头,授权登录用户以及角色等功能.)
            return config;
        },
        'responseError': function (response) {
            if (response.status == 401) {
                var rootScope = $injector.get('$rootScope');
                var state = $injector.get('$rootScope').$state.current.name;
                rootScope.stateBeforLogin = state;
                rootScope.$state.go("/Index");
                return $q.reject(response);
            } else if (response.status === 404) {
                $location.path('/Index');
                return $q.reject(response);
            }
        },
        'response': function (response) {
            return response;
        }
    }
    return httpInterceptor;
}]);


/**
 * 配置路由。
 * 注意这里采用的是ui-router这个路由，而不是ng原生的路由。
 * ng原生的路由不能支持嵌套视图，所以这里必须使用ui-router。
 */
myApp.config(function ($stateProvider, $stickyStateProvider, $urlRouterProvider, $httpProvider) {
    //注入拦截器
    $httpProvider.interceptors.push('httpInterceptor');

    //开启的话,可以知道路由的过渡过程.
    $stickyStateProvider.enableDebug(false);

    $urlRouterProvider.otherwise('');

    var states = [];

    states.push({ name: 'app', url: '', templateUrl: 'home.html' });
    
    states.push({
        name: 'app.stock', url: '/stock',
        views: { 'stock': { template: '<div ui-view=""></div>' } },
        deepStateRedirect: { default: "app.stock.Select" }, sticky: true
    });
    /*查询*/states.push({ name: 'app.stock.Select', url: '/', templateUrl: '../Frontend/Views/Stock.html' });
    /*编辑页面*/states.push({ name: 'app.stock.Edit', url: '/edit/:id', templateUrl: '../Frontend/Views/StockEdit.html' });
    /*新建页面*/states.push({ name: 'app.stock.New', url: '/new', templateUrl: '../Frontend/Views/StockEdit.html' });

    states.push({
        name: 'app.commodityType', url: '/commodityType',
        views: { 'commodityType': { template: '<div ui-view=""></div>' } },
        deepStateRedirect: { default: "app.commodityType.Select" }, sticky: true
    });
    /*查询*/states.push({ name: 'app.commodityType.Select', url: '/', templateUrl: '../Frontend/Views/commodityType.html' });
    /*编辑页面*/states.push({ name: 'app.commodityType..Edit', url: '/edit/:id', templateUrl: ''});

    angular.forEach(states, function (state) { $stateProvider.state(state); });
});



myApp.controller("myAppCtrl", function ($scope, $state) {
    $scope.name = "myApp....";

    //事件 angular-route服务会在状态生命周期的不同阶段触发不同的事件
    //包括1.状态改变事件.2.视图加载事件(begin,now,end)
    $scope.$on('$viewContentLoading',
        function (event, viewConfig) {
            // 在这里可以访问所有视图配置属性
            // 以及一个特殊的“targetView”属性
            // viewConfig.targetView
            //console.log(event);
            //console.log(viewConfig);
        });

    //选项卡
    $scope.getClass = function (name) {
        if ($state.includes('' + name + '')) {
            return "active";
        } else {
            return '';
        }
    }
});

