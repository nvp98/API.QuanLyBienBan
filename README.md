# API Gateway .NET 9 - Clean 3 Layer

## 1. Kiến trúc

```text
Client
   |
   v
+-------------------------+
| ApiGateway.API          |
| Presentation            |
| Controller / Middleware |
+------------+------------+
             |
             v
+-------------------------+
| ApiGateway.Application  |
| Business/Application    |
| Service / Interface/DTO |
+------------+------------+
             |
             v
+-------------------------+
| ApiGateway.Infrastructure|
| YARP / External systems |
+------------+-------------+
             |
             v
      Backend APIs
```

Dependency:

```text
API
 ├──> Application
 └──> Infrastructure

Infrastructure
 └──> Application
```

## 2. Tạo solution

Source đã có sẵn solution và 3 project.

```bash
dotnet restore
dotnet build
dotnet run --project src/ApiGateway.API
```

## 3. Endpoint

```text
GET http://localhost:5001/
GET http://localhost:5001/health
GET http://localhost:5001/api/gateway/info
```

Swagger:

```text
https://localhost:5000/swagger
```

## 4. Reverse Proxy

Gateway:

```text
/api/auth/{**catch-all}
/api/users/{**catch-all}
/api/production/{**catch-all}
```

Backend mặc định:

```text
Auth       -> http://localhost:5101/
User       -> http://localhost:5102/
Production -> http://localhost:5103/
```

Ví dụ:

```text
GET /api/users/123
```

được YARP chuyển đến:

```text
http://localhost:5102/123
```

## 5. Thêm backend

Sửa:

```text
src/ApiGateway.API/appsettings.json
```

Ví dụ thêm:

```json
"mes-route": {
  "ClusterId": "mes-cluster",
  "Match": {
    "Path": "/api/mes/{**catch-all}"
  }
}
```

và:

```json
"mes-cluster": {
  "Destinations": {
    "destination1": {
      "Address": "http://10.192.39.20:5201/"
    }
  }
}
```

## 6. Middleware

Đã có:

- ExceptionMiddleware
- CorrelationIdMiddleware
- X-Correlation-ID

Ví dụ client gửi:

```text
X-Correlation-ID: ABC-123
```

Gateway giữ lại ID này khi xử lý request.

## 7. Rate Limit

Mặc định:

```text
100 request / phút
Queue = 0
```

Vượt giới hạn:

```text
HTTP 429
```

Production có thể chuyển sang Redis để rate limit phân tán.

## 8. Production recommendation

Nên triển khai tiếp:

```text
Client
   |
   v
Nginx / IIS / Load Balancer
   |
   v
API Gateway
   |
   +---- Auth API
   +---- MES API
   +---- Logistics API
   +---- PDA API
   +---- Production API
   +---- Other APIs
```

Gateway nên đảm nhiệm:

- Authentication / JWT
- Authorization
- Rate limiting
- Correlation ID
- Logging
- Audit
- Request/Response policy
- Retry
- Circuit breaker
- Load balancing
- Health check
- Routing

Business logic của từng hệ thống không nên đưa vào Gateway.
