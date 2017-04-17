//库存表 controller:stockCtrl
var myStockModule = (function (my) {

    my = my || {};

    var stockModule = angular.module("stockModule", []);

    stockModule.controller("stockCtrl", ['$scope', 'UrlService', '$location', '$state', '$http', 'DefaultDialogService', 'COMMON_CONSTANT','ngDialog',
        function ($scope, UrlService, $location, $state, $http, DefaultDialogService, COMMON_CONSTANT, ngDialog) {

            //仓库数据模型
            $scope.stockModel = {
                id: '',
                name: '',
                commodityType: '',
                price: '',
                amount: ''
            }

            //编辑页面处理
            $scope.editRun = function () {
                $scope.editName = "编辑";
                var urlJson = UrlService.getUrlParam();
                //获得id
                if (urlJson["id"]) {
                    $scope.stockModel.id = urlJson["id"];
                    //查询id对应的model
                    $http({
                        method: "Get",
                        url: 'api/v1/stock/' + $scope.stockModel.id,
                    }).success(function (data, status, headers, config) {
                        if (_.isUndefined(data) && _.isNull(data)) {
                            $scope.stockModel = data;
                        }
                    }).error(function (data, status, headers, config) {
                        //错误提示
                        DefaultDialogService.open(COMMON_CONSTANT.SERVERFAIL, '错误提示');
                    });
                }
            }

            //新建页面处理
            $scope.newRun = function () {
                //模型清空
                $scope.stockModel = {
                    id: '',
                    name: '',
                    commodityType: '',
                    price: '',
                    amount: ''
                }
                $scope.editName = "新建";
            }

            //查询页面处理
            $scope.selectRun = function () {
                //初始化查询条件
                $scope.initSearchCondition();
                //初始化数据列表
                $scope.initGridsList($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.whereParam);
            }

            //初始化查询条件
            $scope.initSearchCondition = function () {
                $scope.whereParam = { "name": "", "amount": "" };
            };

            //当前页与每页行数初始值
            $scope.pagingOptions = { pageSize: 10, currentPage: 1 };

            //定义Grid
            $scope.gridOptions = {
                data: 'myData',
                columnDefs: [
                    { name: 'id', field: 'id', width: '10%' },
                    { name: '商品名称', field: 'name', width: '36%' },
                    { name: '商品分类', field: 'commodityType', width: '36%' },
                    { name: '商品价格', field: 'price', width: '10%' },
                    { name: '商品数量', field: 'amount', width: '10%' }
                ],
                showSelectionCheckbox: true,
                enableSelectAll: true,
                enableColumnMenus: false,
                enableSelectionBatchEvent: true,
                showGridFooter: false,
                useExternalPagination: true,  //使用分页
                enablePaginationControls: true,  // 显示分页
                paginationPageSizes: [10, 20, 30],
                paginationPageSize: $scope.pagingOptions.pageSize,
                multiSelect: true,
                totalItems: 0,
            };

            $scope.confirmSearch = function () {
                $scope.initGridsList($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.whereParam);
            };

            //初始化grid列表
            $scope.initGridsList = function (pageSize, pageIndex, searchText) {

                $http({
                    method: "GET",
                    url: 'api/v1/stock?currentPage=' + $scope.pagingOptions.currentPage + '&pageSize=' + $scope.pagingOptions.pageSize +'+&name='+ searchText.name + '+&amount='+ searchText.amount+'",
                }).success(function (data, status, headers, config) {

                    if (!_.isUndefined(data) && data != null && data != "") {
                        $scope.myData = data;
                        $scope.gridOptions.totalItems = data.length;
                        $scope.pagingOptions.totalItems = data.length;
                    } else {
                        $scope.myData = [];
                        $scope.pagingOptions.totalItems = 0;
                    }

                    if (!$scope.$$phase) {
                        $scope.$apply();
                    }
                }).error(function (data, status, headers, config) {
                    //错误提示
                    DefaultDialogService.open(COMMON_CONSTANT.SERVERFAIL, '错误提示');
                });
            }

            //注册每页行数改变时的查询事件
            $scope.gridOptions.onRegisterApi = function (gridApi) {
                gridApi.pagination.on.paginationChanged($scope, function (pageNumber, pageSize) {
                    $scope.pagingOptions.currentPage = pageNumber;
                    $scope.pagingOptions.pageSize = pageSize;
                    $scope.initGridsList($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.whereParam);
                });
                $scope.gridApi = gridApi;
            };

            //获取当前选中行
            $scope.getCurrentSelection = function () {
                return $scope.gridApi.selection.getSelectedRows();

            };

            //新建按钮操作
            $scope.goNewBtn = function () {
                //清空模型
                $scope.editName = "新建";
                $scope.newRun();
                $state.go('app.stock.New');

            };
            //编辑按钮操作
            $scope.goEditBtn = function () {
                //是否有选择记录.
                var selectRowCount = $scope.getCurrentSelection().length;
                if (selectRowCount == 0) {

                    DefaultDialogService.open(COMMON_CONSTANT.NORECORD);
                } else {
                    $scope.stockModel = $scope.getCurrentSelection()[0];
                    if ($scope.stockModel.id)
                        $state.go("app.stock.Edit", { id: $scope.stockModel.id });
                }

            };
            //删除按钮操作
            $scope.goDelBtn = function () {
                var selectRowCount = $scope.getCurrentSelection().length;
                if (selectRowCount == 0) {
                    DefaultDialogService.open(COMMON_CONSTANT.NORECORD);
                } else {
                    //NomDefaultDialog.confirm("是否确定删除?").then(
                    //    function () {
                            $scope.stockModel = $scope.getCurrentSelection()[0];
                            if ($scope.stockModel.id) {
                                $http({
                                    method: "Delete",
                                    url: 'api/v1/stock/' + $scope.stockModel.id,
                                }).success(function (data, status, headers, config) {

                                }).error(function (data, status, headers, config) {
                                    //错误提示
                                    DefaultDialogService.open(COMMON_CONSTANT.SERVERFAIL, '错误提示');
                                });
                                }
                        //},
                        //function () {
                        //    console.log('不删除了');
                        //}
                    //);
                }
            }
            //查询按钮
            $scope.goSearchBtn = function () {
                $scope.initGridsList($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.whereParam);
            }


            //初始化页面浏览、编辑状态
            $scope.initPage = function () {
                if ($location.url().indexOf("edit") >= 0) {

                    $scope.editRun();

                } else if ($location.url().indexOf("new") >= 0) {

                    $scope.newRun();

                } else {
                    //查询页面
                    $scope.initSearchCondition();
                    $scope.initGridsList($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.whereParam);
                }
            }();


            //编辑 or  新建页面 
            //保存按钮
            $scope.goSaveBtn = function () {
                if ($scope.editName == "编辑") {
                    if (!_.isNull($scope.stockModel.id) && $scope.stockModel.id!="") {
                        //提交保存
                        $http({
                            method: "Put",
                            url: 'api/v1/stock/',
                            data: $scope.stockModel
                        }).success(function (data, status, headers, config) {

                        }).error(function (data, status, headers, config) {
                            //错误提示
                            //错误提示
                            DefaultDialogService.open(COMMON_CONSTANT.SERVERFAIL, '错误提示');
                        });
                    }
                } else if ($scope.editName == "新建") {
                     //保存成功要把id赋值,且要把新建状态改成编辑状态
                    if (_.isNull($scope.stockModel.id) || $scope.stockModel.id == "") {
                        if (!_.isNull($scope.stockModel.id) && $scope.stockModel.id != "") {
                            //提交保存
                            $http({
                                method: "POST",
                                url: 'api/v1/stock/',
                                data: $scope.stockModel
                            }).success(function (data, status, headers, config) {
                                //返回是数字.需要跳转到浏览页面.
                                if (!_.isUndefined(data) && !_.isNull(data)) {
                                    $scope.stockModel = data;
                                    $scope.editName = "编辑";
                                }
                            }).error(function (data, status, headers, config) {
                                //错误提示
                                //错误提示
                                DefaultDialogService.open(COMMON_CONSTANT.SERVERFAIL, '错误提示');
                            });
                        }
                    }
                }
            }

            //返回按钮
            $scope.goReturnBtn = function () {
                $state.go("app.stock.Select");
            }
        }]);

    return my;

}(myStockModule || {})); 