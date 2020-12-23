#Reference: https://stormpath.com/blog/tutorial-deploy-asp-net-core-on-linux-with-docker
###########################################################################
########################Create Network####################################
docker network ls --no-trunc
docker network create product_network
docker network create k8_app_network

docker inspect sqlstaff -f "{{json .NetworkSettings.Networks }}" 
docker inspect staffservice -f "{{json .NetworkSettings.Networks }}"   # staff
docker inspect sqlproduct -f "{{json .NetworkSettings.Networks }}"   
docker inspect productservice -f "{{json .NetworkSettings.Networks }}"   # Product
docker inspect k8.kubernetesWorld.Service.Employee -f "{{json .NetworkSettings.Networks }}"   # Employee  VS debug 
######################Product API DEPLOYMENT###########################

cd **path**\k8.kubernetesWorld\kubernetesMicroserviceApp\k8.kubernetesWorld.Service.Product
docker build -t k8_product:rc1 .
docker run -d -p 8082:80  --name productservice k8_product:rc1
#Start-Process "http://localhost:8082/swagger"

docker network connect  k8_app_network productservice  
###########################################################################
######################Staff DEPLOYMENT###########################

cd **path**\k8.kubernetesWorld\kubernetesMicroserviceApp\k8.kubernetesWorld.Service.Staff
docker build -t k8_staff:rc1 .
docker run -d -p 8083:80  --name staffservice k8_staff:rc1
#Start-Process "http://localhost:8083/swagger"

docker network connect  k8_app_network staffservice  
###########################################################################
######################Sales DEPLOYMENT - Node##############################

cd **path**\k8.kubernetesWorld\kubernetesMicroserviceApp\k8.kubernetesWorld.Service.Sales
docker build -t k8_sales:rc1 .
docker run -d -p 8085:80  --name salesservice k8_sales:rc1
#Start-Process "http://localhost:8085/health"

docker network connect  k8_app_network salesservice  
###########################################################################
##############################Web DEPLOYMENT###############################

cd **path**\k8.kubernetesWorld\kubernetesMicroserviceApp\k8.kubernetesWorld.Web
docker build -t k8_web:rc1 .
docker run -d -p 8080:80  --name web k8_web:rc1
#Start-Process "http://localhost:8080/home/index"
docker network connect  k8_app_network web
###########################################################################
########################SQL Docker Hub####################################

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=**pwd**" `
   -p 1433:1433 --name mssql-staff `
   -d mcr.microsoft.com/mssql/server:2019-CU3-ubuntu-18.04

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=**pwd**" `
   -p 1432:1433 --name sqlproduct `
   -d mcr.microsoft.com/mssql/server:2019-CU3-ubuntu-18.04

docker exec -it sqlemployee "bash"
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "**pwd**"
SELECT NAME from sys.Databases
Select * from INFORMATION_SCHEMA.TABLES


docker network connect  k8_app_network mssql-staff
docker network connect  k8_app_network sqlproduct
###########################################################################
########################Push Docker Hub####################################
docker login -u=**uid** -p=**pwd**
docker tag k8_client_user:rc2.5 khanasif1/k8_client_user:rc2.5
docker push khanasif1/k8_client_user:rc2.5


docker login -u=$$$$ -p=$$$$
docker tag k8_server_user:rc3 khanasif1/k8_server_user:rc3
docker push khanasif1/k8_server_user:rc3

docker pull khanasif1/k8_server_user:rc3
docker run -d -p 80:80  --name userserverdh khanasif1/k8_server_user:rc3
docker inspect ec4b4807ead8
