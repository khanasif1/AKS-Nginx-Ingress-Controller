'use strict';

var express = require('express'); // Web Framework
var app = express();

const request = require("request");
//const urlProducts = "http://localhost:2148/api/Product";
//const urlStaff = "http://localhost:5652/api/Staff";

const urlProducts = "http://product-service/api/Product";
const urlStaff = "http://staff-service/api/Staff";



 database: 'productDB'
//}
// Start server and listen on http://localhost:8000/
var server = app.listen(80, function () {
    var host = server.address().address
    var port = server.address().port

    console.log("app listening at http://%s:%s", host, port)
});

app.get('/api/sales', function (req, res) {
    var jsonstaffData = "";
    var jsonproductData = "";
    var salesresponse = [];


    request.get(urlProducts, (error, response, body) => {
        jsonproductData = JSON.parse(body);
        request.get(urlStaff, (error, response, body) => {
            jsonstaffData = JSON.parse(body);
            buildResult();
        });    
    }); 

    function buildResult() {      

        for (var x = 0; x < jsonproductData.length; x++) {            
            var seller = {
                sellerid: jsonstaffData[x].id,
                sellername: jsonstaffData[x].firstName + ' ' + jsonstaffData[x].lastName,
                saleitem: jsonproductData[x].id,
                saleitemname: jsonproductData[x].name
            };
            
            salesresponse.push(seller);
        }
        console.log(salesresponse);
        res.send(salesresponse);
    }   
});
app.get('/health', function (req, res) {
    res.sendStatus(200);
});

