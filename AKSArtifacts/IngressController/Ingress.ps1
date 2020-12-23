az --version
helm version

# Use Helm to deploy an NGINX ingress controller
helm repo update
helm install nginx-ingress stable/nginx-ingress   `
--namespace k8-org `
--set controller.replicaCount=1 `
--set controller.nodeSelector."beta\.kubernetes\.io/os"=linux `
--set defaultBackend.nodeSelector."beta\.kubernetes\.io/os"=linux