//库存表 controller:stockCtrl



    var stockModule = angular.module("stockModule", []);
    
    stockModule.controller("stockCtrl", ['$scope', 'UrlService', '$location','$state','$http',
        function ($scope, UrlService, $location, $state, $http) {

            //仓库数据模型
            $scope.stockModel = {
                id: '',
                name: '',
                commodityType: '',
                price: '',
                amount:''
            }


            //初始化页面浏览、编辑状态
            $scope.initPage = function () {
                if ($location.url().indexOf("edit") >= 0) {

                    $scope.editRun();

                } else if ($location.url().indexOf("new") >= 0) {
                    
                    $scope.stockModel = {
                        id: '',
                        name: '',
                        commodityType: '',
                        price: '',
                        amount: ''
                    }
                    $scope.editName = "新建";

                } else {
                    //查询页面
                    $scope.initSearchCondition();
                    $scope.initGridsList($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.whereParam);
                }
            }();

           

            //编辑页面处理
            $scope.editRun = function () {
                $scope.editName = "编辑";
                var urlJson = UrlService.getUrlParam();
                //获得id
                if (urlJson["id"]) {
                    $scope.stockModel.id = urlJson["id"];
                    //查询id对应的model

                }
            };

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
            };

            //查询页面处理
            $scope.selectRun = function () {
                //初始化查询条件
                $scope.initSearchCondition();
                //初始化数据列表
                $scope.initGridsList($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.whereParam);
            }

            //初始化查询条件
            $scope.initSearchCondition = function () {
                $scope.whereParam = { "name": "","amount":""};
            };

            //当前页与每页行数初始值
            $scope.pagingOptions = { pageSize: 10, currentPage: 1 };

            //定义Grid
            $scope.gridOptions = {
                data: 'myData',
                columnDefs: [
                    { name: 'id', field: 'id', width: '10%'},
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
                paginationPageSizes: [10,20,30],
                paginationPageSize: $scope.pagingOptions.pageSize,
                multiSelect: true
            };

            $scope.confirmSearch = function () {
                $scope.initGridsList($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.whereParam);
            };

            //初始化grid列表
            $scope.initGridsList = function (pageSize, pageIndex, searchText) {
                
                $http({
                    method: "GET",
                    url: 'api/shop?currentPage=' + $scope.pagingOptions.currentPage + '&pageSize=' + $scope.pagingOptions.pageSize + '&'
                    + 'name=' + searchText.name + '&amount=' + searchText.amount+'',
                }).success(function (data, status, headers, config) {
                    debugger;
                    //$scope.myData = data.dataResult;
                    //$scope.gridOptions.totalItems = data.total;
                    if (!$scope.$$phase) {
                        $scope.$apply();
                    }
                }).error(function (data, status, headers, config) {
                    //错误提示
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
                $state.go('app.stock.New');
                
            };
            //编辑按钮操作
            $scope.goEditBtn = function () {

            }
            //删除按钮操作
            $scope.goDelBtn = function () {

            }
            //查询按钮
            $scope.goSearchBtn = function () {

            }

        }]);

  

