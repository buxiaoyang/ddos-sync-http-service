# ddos-sync-http-service
Http sync service for dDOS

## Bilder docker image
cd ddos-sync-http-service project folder  
<code>docker build -t buxiaoyang/ddos-sync-http-service .</code>  
or  
<code>docker build -f .\Dockerfile.alpine-x64 -t buxiaoyang/ddos-sync-http-service-alpine .</code>  

run the docker  
<code>docker run -p 8080:80 registry.cn-hangzhou.aliyuncs.com/buxiaoyang/ddos-sync-http-service</code>