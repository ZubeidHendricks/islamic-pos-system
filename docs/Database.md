# Database Schema Documentation

## Tables

### Users
- Id (string, PK)
- UserName (string, unique)
- Email (string, unique)
- PasswordHash (string)
- FirstName (string)
- LastName (string)
- Role (string)
- IsActive (boolean)
- CreatedAt (datetime)
- LastLogin (datetime, nullable)

### Partners
- Id (int, PK)
- Name (string)
- Email (string, nullable)
- SharePercentage (decimal)
- InvestmentAmount (decimal)
- IsActive (boolean)
- JoinDate (datetime)
- LastDistributionDate (datetime, nullable)

### Products
- Id (int, PK)
- Name (string)
- Description (string, nullable)
- Price (decimal)
- StockQuantity (int)
- Barcode (string, unique)
- IsActive (boolean)
- CreatedAt (datetime)
- UpdatedAt (datetime, nullable)

### Sales
- Id (int, PK)
- Date (datetime)
- SubTotal (decimal)
- TaxAmount (decimal)
- Total (decimal)
- PaymentMethod (string)
- CustomerName (string, nullable)

### SaleItems
- Id (int, PK)
- SaleId (int, FK)
- ProductId (int, FK)
- Quantity (int)
- UnitPrice (decimal)
- Total (decimal)

### ProfitDistributions
- Id (int, PK)
- PartnerId (int, FK)
- Amount (decimal)
- DistributionDate (datetime)
- Status (string)
- Reference (string)

## Relationships

### Sales -> SaleItems
- One-to-Many relationship
- Foreign key on SaleItems.SaleId
- Cascade delete

### Partners -> ProfitDistributions
- One-to-Many relationship
- Foreign key on ProfitDistributions.PartnerId
- Restrict delete