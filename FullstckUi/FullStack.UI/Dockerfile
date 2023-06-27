FROM node:18 AS build
WORKDIR /app

COPY package.json /
RUN npm install

COPY ./ /app/
RUN npm run build --prod

FROM nginx:1.21
COPY . .
EXPOSE 80