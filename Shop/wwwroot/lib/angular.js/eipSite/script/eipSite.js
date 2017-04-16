
(function (angular) {
    'use strict';

    var EIPSite = angular.module('EIPSite', []);
    EIPSite.directive('treeSite', [function () {
        return {
            restrict: 'EA',
            replace: 'true',
            transclude: true,
            scope: {
                treeData: '=',
                treeOption: '='
            },
            template: '<div  class="dsite"><input class="form-control" id="txtShowSite" ng-model="treeData.SelectNode.name" ng-click="showTree()" readonly />\
                <span class="caret dr" ></span>\
                       <div  class="sitetree" style="position:absolute;z-index:9;background-color:white;" ng-show="eipShow">\
                            <treecontrol class="tree-classic" options="treeOption" tree-model="treeData" on-selection="showSelected(node)">\
                                    <input type="checkbox" ng-checked="node.checked==1" ng-show="treeOption.multiSelection" />\
                                            {{node.name}}\
                            </treecontrol>\
                        </div>\
				</div>',
            controller: function ($scope) {
                //定义一个以 key/value存储的数据结构
                function Dictionary() {
                    this.data = new Array();
                    this.put = function (key, value) {
                        this.data[key] = value;
                    };

                    this.get = function (key) {
                        return this.data[key];
                    };

                    this.remove = function (key) {
                        this.data[key] = null;
                    };

                    this.isEmpty = function () {
                        return this.data.length == 0;
                    };

                    this.size = function () {
                        return this.data.length;
                    };
                    this.getAll = function () {
                        var ss = '';
                        for (var key in this.data) {
                            ss += key + ',';
                        }
                        return ss;
                    }
                }
                $scope.eipShow = false;
                $scope.selectNodeName = '';
                if ($scope.treeOption.multiSelection) {
                    $scope.treeData.selectedOrgNodes = new Dictionary();
                }
                $scope.arrNodeName = new Dictionary();
                $scope.SelectNodeID = [];
            },
            link: function (scope, element, attrs) {
                //显示还是隐藏组织机构
                scope.showTree = function () {
                    scope.eipShow = !scope.eipShow;
                }
                //组织机构选中事件
                scope.showSelected = function (node) {
                    //判断是否为多选
                    if (!scope.treeOption.multiSelection) {
                        scope.selectNodeName = node.name;
                        scope.treeData.SelectNode = node;
                    } else {
                        //选中和反选CheckBox
                        if (node.checked == "1") {
                            node.checked = "0";
                        } else {
                            node.checked = "1";
                        }
                        //当为选中时，将选中项加入到记录中
                        if (scope.SelectNodeID.indexOf(node.id) < 0) {
                            scope.SelectNodeID.push(node.id);
                            scope.arrNodeName.put(node.id, node.name);
                            scope.treeData.selectedOrgNodes.put(node.id, node);
                        } else {
                            scope.arrNodeName.remove(node.id);
                            scope.treeData.selectedOrgNodes.remove(node.id);
                        }
                        
                        //每次清空需要显示的字符串
                        scope.treeData.SelectNode.name = '';
                        //设置显示字符串
                        for (var i = 0; i < scope.SelectNodeID.length; i++) {
                            if (scope.arrNodeName.get(scope.SelectNodeID[i]) != null) {
                                scope.treeData.SelectNode.name += scope.arrNodeName.get(scope.SelectNodeID[i]) + ",";
                            }
                        }
                    }
                };


            }
        }
    }]);

})(angular);