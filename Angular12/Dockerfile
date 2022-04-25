FROM node:14.17.0-alpine AS build
RUN mkdir -p /app
WORKDIR /app
COPY package.json /app
RUN npm cache clean --force
RUN npm install
COPY . /app
RUN npm run build --prod

FROM nginx AS nginx
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=build app/dist/angular /usr/share/nginx/html
# COPY /nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]
