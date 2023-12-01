# Node.js (Vue 3 Single Page App)
### STAGE 1: Build ###

FROM node:20.10.0 AS build
WORKDIR /app
COPY ./Web.Spa/package*.json ./
RUN npm install
COPY ./Web.Spa/ .
RUN npm run build

### STAGE 2: Run ###
FROM nginx:latest
COPY ./default.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html
