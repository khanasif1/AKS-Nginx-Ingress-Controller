kubectl config set-context --current --namespace=k8-org
<#
========DELETE============
#STAFF
kubectl delete deployment staff -n k8-org
kubectl delete  services staff-service  -n k8-org

#STAFF DB
kubectl delete deployment mssql-staff -n k8-org
kubectl delete  services mssql-staff  -n k8-org

#PRODUCT DB
kubectl delete deployment mssql-product -n k8-org
kubectl delete  services mssql-product  -n k8-org

#PRODUCT
kubectl delete deployment product -n k8-org
kubectl delete  services product-service  -n k8-org

#Sales
kubectl delete deployment sales -n k8-org
kubectl delete  services sales-service  -n k8-org

#WEB
kubectl delete deployment web -n k8-org
kubectl delete  services web-service  -n k8-org
#>

