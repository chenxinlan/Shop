//1.弹出窗口封装
myApp.factory("MyAppDefaultDialogService", ['ngDialog', function (ngDialog) {
    return {
        open: function (msg, title) {
            return ngDialog.open({
                template: 'appTemp/DefaultDialog',
                className: 'ngdialog-theme-default',
                closeByDocument: false,
                showClose: true,
                controller: ['$scope', function ($scope) {
                    if (angular.isUndefined(title)) {
                        title = "消息提示";
                    }

                    $scope.title = title;
                    $scope.msgStr = msg;
                }]
            });
        },
        confirm: function (msg, title) {
            return ngDialog.openConfirm({
                template: 'appTemp/ConfirmDialog',
                className: 'ngdialog-theme-default',
                closeByDocument: false,
                showClose: true,
                controller: ['$scope', function ($scope) {
                    if (angular.isUndefined(title)) {
                        title = "消息提示";
                    }
                    $scope.title = title;
                    $scope.msgStr = msg;
                }]
            });
        }
    };
}]);


//2.post请求提交封装
myApp.factory("MyAppHttpFilterService", ['$http', function ($http) {
    return {
        GET: function (httpObject) {//
            return $http({
                method: "GET",
                url: httpObject.url,
                data: httpObject.params
            })
        },
        post: function (httpObject) {
            //可以对请求字符串做通用的处理
            return $http({
                method: "POST",
                url: httpObject.url,
                data: httpObject.params
            })
        },
    };
}]);

//3.url参数解析
myApp.service('UrlService', ['$location', function ($location) {
    return {
        //该函数只针对符合路由参数设置的"transParam=json串"格式的URL
        getUrlParam: function () {
            var aTmpParams = $location.url().toString().split('?');
            var strParam = "{}";
            if (aTmpParams.length > 1) {
                aTmpParams[1] = decodeURI(aTmpParams[1]);
                var arr1 = aTmpParams[1].split('=');
                strParam = arr1[1];
            }
            //将参数转化为Json对象
            return JSON.parse(strParam);
        }
    }

}]);


//4.弹出窗口封装
myApp.factory("DefaultDialogService", ['ngDialog', function (ngDialog) {
    return {
        open: function (msg, title) {
            return ngDialog.open({
                template: 'appTemp/DefaultDialog',
                className: 'ngdialog-theme-default',
                closeByDocument: false,
                showClose: true,
                controller: ['$scope', function ($scope) {
                    if (angular.isUndefined(title)) {
                        title = "消息提示";
                    }

                    $scope.title = title;
                    $scope.msgStr = msg;
                }]
            });
        },
        confirm: function (msg, title) {
            return ngDialog.openConfirm({
                template: 'appTemp/ConfirmDialog',
                className: 'ngdialog-theme-default',
                closeByDocument: false,
                showClose: true,
                controller: ['$scope', function ($scope) {
                    if (angular.isUndefined(title)) {
                        title = "消息提示";
                    }
                    $scope.title = title;
                    $scope.msgStr = msg;
                }]
            });
        }
    };
}]);
