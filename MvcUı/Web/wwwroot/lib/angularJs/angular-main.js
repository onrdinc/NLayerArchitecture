var app = angular.module('app', []);




app.directive('format', ['$filter', function ($filter) {
    return {
        require: '?ngModel',
        link: function (scope, elem, attrs, ctrl) {
            if (!ctrl) return;

            ctrl.$formatters.unshift(function (a) {
                return $filter(attrs.format)(ctrl.$modelValue, '')
            });

            elem.bind('blur', function (event) {
                var plainNumber = elem.val().replace(/[^\d|\-+|\.+]/g, '');
                elem.val($filter(attrs.format)(plainNumber, ''));
            });
        }

    };
}]);

app.filter('groupBy', function () {
    return function (input, key) {
        if (!key) return input;

        var output = {};
        for (var i = 0; i < input.length; i++) {
            if (!output[input[i][key]]) {
                output[input[i][key]] = [];
            }
            output[input[i][key]].push(input[i]);
        }
        return output;
    };
});

//app.config(function ($routeProvider) {
//    $routeProvider


//        .when("/", {
//            templateUrl: "/templates/signIn.html",
//            title: 'Oturum Aç'
//        })
//        .when("/signUp", {
//            templateUrl: "/templates/signUp.html",
//            title: 'Kaydol'
//        })
//        .when("/home", {
//            templateUrl: "/templates/home.html",
//            title: 'Ana Sayfa'
//        })
//        .when("/:organizationUrl/:url", {
//            templateUrl: "/templates/companyDetail.html",
//            title: 'Şirket Detayı'
//        })
      
//        .otherwise({
//            redirectTo: "/",
//            title: 'Purchasing'
//        });
//});

//app.run(['$location', '$rootScope', '$routeParams', function ($location, $rootScope, $routeParams) {



//    $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {

//        if ($routeParams.url != undefined) {
//            $rootScope.title = $routeParams.url;
//        }
//        else {
//            $rootScope.title = current.$$route.title;
//        }

//    });
//}]);

app.controller('controller', function ($scope, $http, $location) {

    $scope.apiLocation = 'https://localhost:7016/api';



    $scope.companyRoles = {
        accountType: '',
        roles: ''
    };

    $scope.alert = {
        item: {},
        action: function (response) {
            this.item = response;
            $('#alertModal').modal('show');
        }
    };

    $scope.groupBy = function (array, key) {
        return array.reduce(function (result, item) {
            var groupKey = item[key];
            if (!result[groupKey]) {
                result[groupKey] = [];
            }
            result[groupKey].push(item);
            return result;
        }, {});
    };

    $scope.clear = function (fd) {
        var formData = new FormData();

        for (var pair of fd.entries()) {

            if (typeof pair[1] != 'object') {
                pair[1] = pair[1].replace(null, '');
                pair[1] = pair[1].replace('undefined', '');
            }


            formData.append(pair[0], pair[1]);
        }
        return formData;
    };

    $scope.dateFormat = function (date) {

        return $filter('date')(date, 'yyyy-MM-dd');
    };


   
    $scope.getDateDiffText = function (d) {
        var currentDate = new Date();
        var maxUpdateDate = new Date(d);

        var seconds = Math.floor((currentDate - maxUpdateDate) / 1000);
        var years = seconds / 31536000;
        var months = seconds / 2592000;
        var days = seconds / 86400;
        var hours = seconds / 3600;
        var minutes = seconds / 60;
        // console.log('getDateDiffText', seconds, years, months, days, hours, minutes);
        if (seconds < 0) {
            if (years < -1) {
                return Math.floor(years) + " yıl sonra";
            }
            if (months < -1) {
                return Math.floor(months) + " ay sonra";
            }
            if (days < -1) {
                return Math.floor(days) + " gün sonra";
            }
            if (hours < -1) {
                return Math.floor(hours) + " saat sonra";
            }
            if (minutes < -1) {
                return Math.floor(minutes) + " dakika sonra";
            }
            return Math.floor(seconds) + " saniye sonra";
        } else {
            if (years > 1) {
                return Math.floor(years) + " yıl önce";
            }
            if (months > 1) {
                return Math.floor(months) + " ay önce";
            }
            if (days > 1) {
                return Math.floor(days) + " gün önce";
            }
            if (hours > 1) {
                return Math.floor(hours) + " saat önce";
            }
            if (minutes > 1) {
                return Math.floor(minutes) + " dakika önce";
            }
            return Math.floor(seconds) + " saniye önce";
        }
    };

    $scope.fnUser = function () {

        var obj = {
            filter: {
                search: '',
                userId: '',

            },
            isLoading: false,
            method: '',
            detail: [],
            list: [],
            userlist: [],
            employee: [],
            employeeList: [],
            current: null,
            add: function () {

                obj.method = 'POST';
                obj.detail = [];
                obj.detail.push({ password: '', email: '', firstName: '', userName: '', lastName: ''});


            },
            token: function () {
                if (!obj.isLoading) {
                    obj.isLoading = true;
                }
                var fd = new FormData();
                fd.append('Email', obj.detail[0].email);
                fd.append('Password', obj.detail[0].password);

                $http({
                    method: 'POST',
                    url: $scope.apiLocation + '/user/token',
                    data: $scope.clear(fd),
                    headers: {
                        'Content-Type': undefined, 

                    }
                }).then(function successCallback(response) {
                    if (response.status == 200) {
                        $scope.fnData.set('token', response.data.item.token);
                        location.href = '/Bank/Index'
                        //$location.path('/dashboard')

                        //location.href = '/#!/home';
                    } else {
                        $scope.alert.action(response);
                    }
                    obj.isLoading = false;
                }, function errorCallback(response) {
                    $scope.alert.action(response);
                    obj.isLoading = false;
                });
            },

            //frombody
            //token: function () {
            //    if (!obj.isLoading) {
            //        obj.isLoading = true;
            //    }

            //    var requestData = {
            //        Email: obj.detail[0].email,
            //        Password: obj.detail[0].password
            //    };

            //    $http({
            //        method: 'POST',
            //        url: $scope.apiLocation + '/user/token',
            //        data: requestData, // Send the data as a JSON object
            //        headers: {
            //            'Content-Type': 'application/json', // Set the content type to JSON
            //        }
            //    }).then(function successCallback(response) {
            //        if (response.status == 200) {
            //            $scope.fnData.set('token', response.data.item.token);
            //            obj.myProfile();
            //            //$location.path('/dashboard')
            //            //location.href = '/#!/home';
            //        } else {
            //            $scope.alert.action(response);
            //        }
            //        obj.isLoading = false;
            //    }, function errorCallback(response) {
            //        $scope.alert.action(response);
            //        obj.isLoading = false;
            //    });
            //},

            //token: function () {
            //    if (!obj.isLoading) {
            //        obj.isLoading = true;
            //    }
            //    var fd = new FormData();
            //    fd.append('Email', obj.detail[0].email);
            //    fd.append('Password', obj.detail[0].password);

            //    $http({
            //        method: 'POST',
            //        url: "/Authentication/LogIn",
            //        data: $scope.clear(fd),
            //        dataType:"json",
            //        headers: {
            //            'Content-Type': undefined,

            //        }
            //    }).then(function successCallback(response) {
            //        if (response.status == 200) {
            //            console.log(response,'response');
            //            $scope.fnData.set('token', response.data.item.token);
            //            //obj.myProfile();
            //            //$location.path('/dashboard')

            //            //location.href = '/#!/home';
            //        } else {
            //            $scope.alert.action(response);
            //        }
            //        obj.isLoading = false;
            //    }, function errorCallback(response) {
            //        $scope.alert.action(response);
            //        obj.isLoading = false;
            //    });

            //},







           
            logOut: function () {
                obj.isLoading = true;
                $scope.fnData.set('token', '');
                location.href = '/';
            },

        };
        return obj;
    };
    $scope.user = $scope.fnUser();


    $scope.fnBank = function () {
        var obj = {
            filter: {
                search: '',
            },
            isLoading: false,
            method: '',
            datail: [],
            list: [],
            count: 0,
            add: function () {
                obj.method = 'POST';
                obj.detail = [{}];
                $('#departmentAddFormModal').modal('show');
            },
            save: function () {
                if (!obj.isLoading) {
                    obj.isLoading = true;
                }
                var token = $scope.fnData.get('token');
                var fd = new FormData();

                fd.append('Id', obj.detail[0].id);
                fd.append('Name', obj.detail[0].name);
                //fd.append('IsCanBuy', obj.detail[0].isCanBuy);



                $http({
                    method: obj.method,
                    url: $scope.apiLocation + '/department',
                    data: $scope.clear(fd),
                    headers: {
                        'Content-Type': undefined,
                        'Authorization': 'Bearer ' + token
                    }
                }).then(function successCallback(response) {
                    if (response.status == 200) {
                        $('#departmentAddFormModal').modal('hide');
                        $scope.alert.action(response);
                        obj.multipleGet();

                    } else {
                        $scope.alert.action(response);
                    }
                    obj.isLoading = false;
                }, function errorCallback(response) {
                    $scope.alert.action(response);
                    obj.isLoading = false;
                });

            },
            multipleGet: function () {
                obj.isLoading = true;
                var token = $scope.fnData.get('token');

                var fd = new FormData();

                fd.append('Search', obj.filter.search)


                $http({
                    method: 'POST',
                    url: "/Bank/GetBankData",
                    data: $scope.clear(fd),
                    headers: {
                        'Content-Type': 'application/json',

                        'Authorization': token
                    }
                }).then(function successCallback(response) {
                    console.log('response', response);
                    console.log('response.data', response.data);
                    console.log('response.data.item', response.data.item);

                    if (response.status == 200) {
                        obj.list = [];
                        //obj.list = response.data;
                        angular.forEach(response.data.item, function (value, key) {
                            obj.list.push(value);
                        });
                        obj.count = response.data.count;
                    } else {
                        $scope.alert.action(response);
                    }
                    obj.isLoading = false;
                }, function errorCallback(response) {
                    $scope.alert.action(response);
                    obj.isLoading = false;
                });
            },
            singleGet: function (id) {
                var token = $scope.fnData.get('token');
                $http({
                    method: 'GET',
                    url: $scope.apiLocation + '/department/' + id,
                    headers: {
                        'Content-Type': undefined,
                        'Authorization': 'Bearer ' + token
                    }
                }).then(function successCallback(response) {
                    if (response.status == 200) {
                        obj.method = 'PUT';
                        obj.detail = [];
                        obj.detail.push(response.data.item);
                        console.log(response.data.item);
                        $('#departmentAddFormModal').modal('show');
                    } else {

                    }

                }, function errorCallback(response) {
                });

            },
            delete: function (id) {
                var token = $scope.fnData.get('token');

                if (confirm('Silmek istediğine emin misin?')) {
                    $http({
                        method: 'DELETE',
                        url: $scope.apiLocation + '/bank/' + id,

                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': token
                        }
                    }).then(function successCallback(response) {

                        if (response.status == 200) {
                            $scope.alert.action(response);
                            obj.multipleGet();
                        } else {

                        }
                        $scope.alert.action(response);
                        obj.isLoading = false;

                    }, function errorCallback(response) {
                        $scope.alert.action(response);
                        obj.isLoading = false;

                    });

                }
            },
        };
        return obj;
    };
    $scope.bank = $scope.fnBank();



    $scope.fnUrlFormat = function () {
        var obj = {
            urlText: function (data) {
                var strReturn = data;
                for (var i = 0; i < 3; i++) {
                    strReturn = data;

                    strReturn = String(strReturn).toLowerCase();

                    strReturn = String(strReturn).replace(/\?/g, "");
                    strReturn = String(strReturn).replace(/\&/g, "");
                    strReturn = String(strReturn).replace(/\!/g, "");
                    strReturn = String(strReturn).replace(/\%/g, "");
                    strReturn = String(strReturn).replace(/\=/g, "");
                    strReturn = String(strReturn).replace(/\:/g, "");
                    strReturn = String(strReturn).replace(/\;/g, "");
                    strReturn = String(strReturn).replace(/\;/g, "");
                    strReturn = String(strReturn).replace(/\;/g, "");
                    strReturn = String(strReturn).replace(/\"/g, "");
                    strReturn = String(strReturn).replace(/\$/g, "");
                    strReturn = String(strReturn).replace(/\'/g, "");
                    strReturn = String(strReturn).replace(/\(/g, "");
                    strReturn = String(strReturn).replace(/\)/g, "");
                    strReturn = String(strReturn).replace(/\*/g, "");
                    strReturn = String(strReturn).replace(/\+/g, "");
                    strReturn = String(strReturn).replace(/\./g, "");
                    strReturn = String(strReturn).replace(/\//g, "");
                    strReturn = String(strReturn).replace(/\</g, "");
                    strReturn = String(strReturn).replace(/\>/g, "");
                    strReturn = String(strReturn).replace(/\@/g, "");
                    strReturn = String(strReturn).replace(/\[/g, "");
                    strReturn = String(strReturn).replace(/\]/g, "");
                    strReturn = String(strReturn).replace(/\_/g, "");
                    strReturn = String(strReturn).replace(/\~/g, "");
                    strReturn = String(strReturn).replace(/\^/g, "");
                    strReturn = String(strReturn).replace(/\{/g, "");
                    strReturn = String(strReturn).replace(/\}/g, "");
                    strReturn = String(strReturn).replace(/\|/g, "");
                    strReturn = String(strReturn).replace(/\,/g, "");
                    strReturn = String(strReturn).replace(/\"/g, "");

                    strReturn = String(strReturn).replace(/ğ/g, "g");
                    strReturn = String(strReturn).replace(/Ğ/g, "G");
                    strReturn = String(strReturn).replace(/ü/g, "u");
                    strReturn = String(strReturn).replace(/Ü/g, "U");
                    strReturn = String(strReturn).replace(/ş/g, "s");
                    strReturn = String(strReturn).replace(/Ş/g, "S");
                    strReturn = String(strReturn).replace(/ı/g, "i");
                    strReturn = String(strReturn).replace(/i̇/g, "i");
                    strReturn = String(strReturn).replace(/İ/g, "i");
                    strReturn = String(strReturn).replace(/ö/g, "o");
                    strReturn = String(strReturn).replace(/Ö/g, "O");
                    strReturn = String(strReturn).replace(/ç/g, "c");
                    strReturn = String(strReturn).replace(/Ç/g, "C");
                    strReturn = String(strReturn).replace(/ /g, "-");
                    strReturn = String(strReturn).replace(/------/g, "-");
                    strReturn = String(strReturn).replace(/-----/g, "-");
                    strReturn = String(strReturn).replace(/----/g, "-");
                    strReturn = String(strReturn).replace(/---/g, "-");
                    strReturn = String(strReturn).replace(/--/g, "-");
                    strReturn = String(strReturn).replace(/\+\+\+\++/g, "-");
                    strReturn = String(strReturn).replace(/\+\+\++/g, "-");
                    strReturn = String(strReturn).replace(/\++\++/g, "-");
                    strReturn = String(strReturn).replace(/\++\+/g, "-");
                    strReturn = String(strReturn).replace(/\++/g, "-");
                    strReturn = String(strReturn).replace(/\+/g, "-");
                }
                return strReturn;
            }
        }
        return obj;
    }
    $scope.urlFormat = $scope.fnUrlFormat();

    $scope.fnData = {
        set: function (key, val) {
            localStorage.setItem('purchasing-' + key, val);
        },
        get: function (key) {
            var val = localStorage.getItem('purchasing-' + key);
            return val
        }
    };

    //$scope.user.currentUserGet();


});

