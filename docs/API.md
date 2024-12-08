# Islamic POS System API Documentation

## Authentication

### POST /api/auth/login
Authenticate a user and get JWT token.

**Request:**
```json
{
    "username": "string",
    "password": "string"
}
```

**Response:**
```json
{
    "success": true,
    "token": "jwt-token-string",
    "user": {
        "id": "string",
        "username": "string",
        "email": "string",
        "role": "string",
        "permissions": ["string"]
    }
}
```

## Sales Management

### POST /api/sales
Create a new sale transaction.

**Request:**
```json
{
    "items": [
        {
            "productId": 0,
            "quantity": 0,
            "unitPrice": 0
        }
    ],
    "paymentMethod": "string",
    "customerName": "string"
}
```

### GET /api/sales/daily-summary
Get sales summary for a specific date.

**Parameters:**
- date (query): Date to get summary for

## Zakaah Calculation

### POST /api/zakaah/calculate
Calculate Zakaah based on provided assets.

**Request:**
```json
{
    "cashOnHand": 0,
    "inventoryValue": 0,
    "otherAssets": 0,
    "currency": "USD"
}
```

**Response:**
```json
{
    "totalAssets": 0,
    "isEligible": true,
    "zakaahAmount": 0,
    "currency": "USD",
    "calculationDate": "2024-12-08T00:00:00Z"
}
```

## Profit Distribution

### POST /api/profits/calculate
Calculate profit distribution for partners.

**Request:**
```json
{
    "startDate": "2024-01-01T00:00:00Z",
    "endDate": "2024-12-31T23:59:59Z"
}
```

**Response:**
```json
{
    "totalRevenue": 0,
    "totalCosts": 0,
    "netProfit": 0,
    "shares": [
        {
            "partnerId": 0,
            "name": "string",
            "percentage": 0,
            "amount": 0
        }
    ]
}
```

## Reports

### GET /api/reports/sales
Get detailed sales report.

**Parameters:**
- startDate (query): Start date for report
- endDate (query): End date for report
- format (query, optional): "pdf" or "excel"

### GET /api/reports/profit
Get profit analysis report (Admin/Manager only).

**Parameters:**
- startDate (query): Start date for report
- endDate (query): End date for report

## Error Responses

All endpoints may return the following error responses:

### 400 Bad Request
```json
{
    "message": "Error description",
    "errors": {
        "field": ["error messages"]
    }
}
```

### 401 Unauthorized
```json
{
    "message": "Invalid credentials or token expired"
}
```

### 403 Forbidden
```json
{
    "message": "Insufficient permissions"
}
```

### 500 Internal Server Error
```json
{
    "message": "An error occurred while processing your request"
}
```

## Authentication

All endpoints except `/api/auth/login` require a valid JWT token in the Authorization header:

```
Authorization: Bearer <token>
```

## Permissions

The following roles have access to specific endpoints:

- Admin: Full access to all endpoints
- Manager: Access to sales, reports, and profit distribution
- Cashier: Access to sales endpoints only

## Rate Limiting

API requests are limited to:
- 100 requests per minute for authenticated users
- 20 requests per minute for unauthenticated users