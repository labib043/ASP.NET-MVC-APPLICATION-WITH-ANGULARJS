

var app = angular.module("myEvie", []);
app.controller("EmployeesController", function ($scope, $http, $rootScope) {

    $scope.title = 'Employees';
    $scope.IdDisable = true;
    $scope.Id = 0;
    $scope.IsActive = true;
    $scope.JoiningDate = new Date();
 

    $scope.tabledata = function () {

        $http.get('/Employees/getAllData').then(

            function (res) {

                $scope.tabledataholder = res.data;                
                console.log($scope.tabledataholder);
                
            }
            , function (err) {

                console.log(err);

            }
        );
    }

    $scope.tabledata();

    console.log('companyNalekekke');

    $scope.Companydata = function () {
        console.log('companyNalekekke');
        $http.get('/Genders/getAllData').then(

            function (res) {

                $rootScope.Companydataholder = res.data;
                console.log($scope.Companydataholder);

            }
            , function (err) {

                console.log(err);

            }
        );
    }
    $scope.Companydata();




    $scope.edit = function (Id, Name, IsActive, JoiningDate, GenderId) {

        document.getElementById("GenderId").value = GenderId;
        $scope.Id = Id;
        $scope.Name =Name;
        $scope.IsActive = IsActive;
        var tempDate = JoiningDate.slice(6, -2);
        $scope.JoiningDate = new Date(Number(tempDate));
        console.log($scope.JoiningDate);     
        $scope.$apply(function (GenderId) {
            $scope.GenderId = GenderId;
        })
            
    }



    $scope.addToTable = function (_Id, _Name, _IsActive, _JoiningDate, _GenderId) {

        var fdata = new FormData();

        fdata.Id = $scope.Id;
        fdata.Name = $scope.Name;                    
        fdata.IsActive = $scope.IsActive;
        fdata.JoiningDate = $scope.JoiningDate;
        fdata.GenderId = $scope.GenderId;

        console.log(fdata);


        $http.post('/Employees/AddEmployee', JSON.stringify(fdata)).then(

            function (res) {

                $scope.message = res.data;
                console.log($scope.message);
                $scope.tabledata();
                $scope.Id = 0;
                $scope.Name = '';


            }
            , function (err) {

                $scope.message = err;

            }
        );

    }


    $scope.editToTable = function (Id, Name, IsActive, JoiningDate, GenderId) {

        var fdata = new FormData();

        fdata.Id = Id;
        fdata.Name = Name;        
        fdata.IsActive = IsActive;
        fdata.JoiningDate = JoiningDate;
        fdata.GenderId = GenderId;

        $http.post('/Employees/UpdateEmployee', JSON.stringify(fdata)).then(

            function (res) {

                $scope.message = res.data;
                console.log($scope.message);
                $scope.tabledata();
                $scope.Id = 0;
                $scope.Name = '';


            }
            , function (err) {

                $scope.message = err;

            }
        );

    }


    $scope.deleteToTable = function (Id, Name, IsActive, JoiningDate, GenderId) {

        var paramObject = { Id: Id, Name: Name, IsActive: IsActive, JoiningDate: JoiningDate };

        $http.post('/Employees/Delete', paramObject).then(

            function (res) {

                $scope.message = res.data;
                console.log($scope.message);
                $scope.tabledata();
                $scope.Id = 0;
                $scope.Name = '';


            }
            , function (err) {

                $scope.message = err;

            }
        );

    }


   


});


app.controller("GendersController", function ($scope, $http, $rootScope) {
    $scope.title = 'Genders';
    $scope.addToTable = function () {

        var fdata = new FormData();
        fdata.Name = $scope.Name;

        console.log(fdata);

        $http.post('/Genders/AddGender', JSON.stringify(fdata)).then(

            function (res) {

                $scope.message = res.data;
                $scope.Companydata();

            }
            , function (err) {

                $scope.message = err;

            }
        );

    }

    $scope.Companydata = function () {
        console.log('companyNalekekke');
        $http.get('/Genders/getAllData').then(

            function (res) {

                $rootScope.Companydataholder = res.data;
                console.log($scope.Companydataholder);

            }
            , function (err) {

                console.log(err);

            }
        );
    }
    $scope.Companydata();

    $scope.edit = function (Id, Name) {
        $scope.Id = Id;
        $scope.Name = Name;

    }

    //$scope.delete = function (Id, Name) {
    //    $scope.IdDisable = true;
    //    $scope.Id = Id;
    //    $scope.Name = Name;



    //}
    $scope.editToTable = function (Id, Name) {
        var Fdata = new FormData();
        Fdata.Id = $scope.Id;
        Fdata.Name = $scope.Name;

        $http.post('/Genders/UpdateGender', JSON.stringify(Fdata)).then(
            function (res) {
                $scope.massage = res.data;
                console.log($scope.massage);
                $scope.Companydata();
                $scope.Id = 0;
                $scope.Name = '';
            }
            , function (err) {
                $scope.massage = err;
            }
        );
    }


    $scope.deleteToTable = function (Id, Name) {

        var paramObject = { Id: Id, Name: Name };

        $http.post('/Genders/DeleteGender', paramObject).then(

            function (res) {

                $scope.message = res.data;
                console.log($scope.message);
                $scope.Companydata();
                $scope.Id = 0;
                $scope.Name = '';


            }
            , function (err) {

                $scope.message = err;

            }
        );

    }




});