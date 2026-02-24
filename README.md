# Redarbor API

API REST desarrollada con .net 8 siguiendo los principios de arquitectura limpia DDD y CQRS.

---

## Arquitectura:
La solución esta estructurada en las siguientes capas:

- **Redarbor.Domain** -> Core de negocio.
- **Redarbor.Application** -> Casos de uso y DTOs.
- **Redarbor.Infrastructure** -> Servicios especificos o externos.
- **Redarbor.Persistence** -> Contexto y Repositorios.
- **Redarbor.API** -> API Web que contiene los endpoints.

---

## Tecnologías

- .Net 8
- Entity Framework Core,
- Dapper
- Sql Server
- Swagger
- Docker

---

## Ejecución desde Docker

La API esta configurada para que desde visual studio y docker desktop sea solamente presionar la tecla F5, automaticamente:
-Levantara contenedores y dependencias.
-Validará y realizará migraciones.
-Inicializará datos del usuario administrador con el que se podrá trabajar en la api para la creación de nuevos empoleados y usuarios.
Login Admin:
{
  "userName": "nicolas.bonilla",
  "password": "RedArbor2026@"
}
-Abrira el navegador para la utilización de la API.
-Se ecuentra configurada bajo el puerto 5000.
http://localhost:5000/swagger

## Autor
  - Nicolás Bonilla
