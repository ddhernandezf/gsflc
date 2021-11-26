FROM node:14-alpine as appBuilder
RUN npm i -g npm@latest
ARG ENVVAR
WORKDIR /usr/src/app
COPY package*.json ./
RUN npm install
COPY . ./
RUN npm run build -- --outputPath=./deploy --configuration $ENVVAR

FROM nginx:1.21-alpine
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=appBuilder /usr/src/app/deploy /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
