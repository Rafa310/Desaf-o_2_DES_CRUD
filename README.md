# Desafio Practico #2 - Gestion de Eventos, Participantes y Organizadores

API CRUD con Dapper + cache de lectura en Redis, expuesta a traves de un API Gateway con Ocelot
(rate limiting + cache), todo contenedorizado con Docker y SQL Server.

# Integrantes
```
Angel Eduardo Moreno Escobar  -   ME220001
Rafael Adolfo Ruiz Garcia     -   RG210380
Ricardo Ivan Escobar Umaña    -   EU220488
```

## Estructura del proyecto

```
Desafio_2.slnx
docker-compose.yml
EventosApi/          -> API con Dapper (enfoque N-Capas de la Guia #4, en un solo proyecto)
  Common/            -> AppSettings, DbInitializer (crea BD/tablas/datos al arrancar)
  Models/            -> Entidades de dominio (Evento, Participante, Organizador)
  DTO/               -> DTOs con las validaciones del enunciado
  DAL/                -> Repositorio generico (Dapper) + repositorios por entidad
  BL/                 -> Servicios de negocio + AutoMapper + cache Redis (30s en lecturas)
  Controllers/        -> Controladores REST (GET/POST/PUT/DELETE por entidad)
  Scripts/schema.sql  -> Script SQL Server (tablas + datos semilla)
ApiGateway/           -> Gateway con Ocelot (rate limit 10 req/min + cache 10s por endpoint)
  ocelot.json
```

## Como levantar todo con Docker

Desde la carpeta raiz del proyecto:

```bash
docker compose up -d --build
```

Esto levanta 4 contenedores: `sqlserver`, `redis`, `eventosapi` (puerto 5001) y `apigateway`
(puerto 5000). La API crea la base de datos, las tablas y los datos de ejemplo automaticamente
la primera vez que arranca (ver logs con `docker compose logs eventosapi`).

Para bajar todo: `docker compose down` (agregar `-v` si tambien se quiere borrar el volumen de
SQL Server).

## Endpoints

- API directa: `http://localhost:5001/api/eventos`, `/api/participantes`, `/api/organizadores`
- Documentacion interactiva (Scalar): `http://localhost:5001/scalar/`
- A traves del Gateway: `http://localhost:5000/api/eventos`, etc.

**Importante:** el Gateway exige un header `ClientId` (cualquier valor) en cada peticion para
poder contabilizar el rate limit por cliente. Ejemplo:

```bash
curl -H "ClientId: alumno1" http://localhost:5000/api/eventos
```

Sin ese header, Ocelot no puede identificar al cliente y rechaza la peticion.
