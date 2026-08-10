CREATE DATABASE Ecommerce_DB;
GO
DROP DATABASE Ecommerce_DB ;

USE Ecommerce_DB;
GO
select * from orders;
SELECT name
FROM sys.types
WHERE is_table_type = 1;
UPDATE Categories
SET CategoryName = 'Indian Perfumes'
WHERE CategoryId = 1;

UPDATE Categories
SET CategoryName = 'Oudh And Bhakdoors'
WHERE CategoryId = 2;

UPDATE Categories
SET CategoryName = 'Oils'
WHERE CategoryId = 3;

UPDATE Categories
SET CategoryName = 'Body Mist'
WHERE CategoryId = 4;

UPDATE Categories
SET CategoryName = 'Hair Mist'
WHERE CategoryId = 7;

SELECT
    p.ProductId,
    p.ProductName,
    p.CategoryId,
    c.CategoryName
FROM Products p
JOIN Categories c
    ON p.CategoryId = c.CategoryId
WHERE c.CategoryName = 'Niche Perfumes';
----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Users
(
    UserId INT PRIMARY KEY IDENTITY(1,1),

    Username VARCHAR(100) NOT NULL,

    Email VARCHAR(255) NOT NULL UNIQUE,

    PasswordHash VARCHAR(MAX) NULL,

    Role VARCHAR(20) NOT NULL DEFAULT 'User',

    IsVerified BIT NOT NULL DEFAULT 0,

    VerificationCode VARCHAR(10) NULL,

    GoogleId VARCHAR(255) NULL,

    FacebookId VARCHAR(255) NULL,

    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),

    UpdatedAt DATETIME2 NULL,

    LoginProvider VARCHAR(20) NOT NULL
);
----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Categories
(
    CategoryId INT PRIMARY KEY IDENTITY(1,1),

    CategoryName VARCHAR(100) NOT NULL UNIQUE
);
----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Brands
(
    BrandId INT PRIMARY KEY IDENTITY(1,1),

    BrandName VARCHAR(100) NOT NULL UNIQUE
);
----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Products
(
    ProductId INT PRIMARY KEY IDENTITY(1,1),

    ProductName VARCHAR(255) NOT NULL,

    CategoryId INT NOT NULL,

    BrandId INT NOT NULL,

    Description VARCHAR(MAX),

    VolumeMl INT,

    Gender VARCHAR(20),

    Price DECIMAL(10,2) NOT NULL,

    Discount DECIMAL(5,2) DEFAULT 0,

    CreatedAt DATETIME2 DEFAULT GETDATE(),

    Updated DATETIME2 NULL,

    CONSTRAINT FK_Product_Category
        FOREIGN KEY(CategoryId)
        REFERENCES Categories(CategoryId),

    CONSTRAINT FK_Product_Brand
        FOREIGN KEY(BrandId)
        REFERENCES Brands(BrandId)
);
----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Images
(
    ImageId INT PRIMARY KEY IDENTITY(1,1),

    ProductId INT NOT NULL,

    ImageUrl VARCHAR(MAX) NOT NULL,

    ImagePublicId VARCHAR(255) NOT NULL,

    CONSTRAINT FK_Image_Product
        FOREIGN KEY(ProductId)
        REFERENCES Products(ProductId)
        ON DELETE CASCADE
);
----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Reviews
(
    ReviewId INT PRIMARY KEY IDENTITY(1,1),

    ProductId INT NOT NULL,

    UserId INT NOT NULL,

    Rating INT NOT NULL
        CHECK(Rating BETWEEN 1 AND 5),

    ReviewText VARCHAR(1000),

    ReviewDate DATETIME2 DEFAULT GETDATE(),

    UpdatedAt DATETIME2 NULL,

    CONSTRAINT FK_Review_Product
        FOREIGN KEY(ProductId)
        REFERENCES Products(ProductId)
        ON DELETE CASCADE,

    CONSTRAINT FK_Review_User
        FOREIGN KEY(UserId)
        REFERENCES Users(UserId),

    CONSTRAINT UQ_Product_User
        UNIQUE(ProductId,UserId)
);
----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Orders
(
    OrderId INT PRIMARY KEY IDENTITY(1,1),

    UserId INT NOT NULL,

    OrderDate DATETIME2 DEFAULT GETDATE(),

    TotalAmount DECIMAL(10,2),

    Status VARCHAR(30),

    ShippingAddress NVARCHAR(500) NOT NULL DEFAULT,

    CONSTRAINT FK_Order_User
        FOREIGN KEY(UserId)
        REFERENCES Users(UserId)
);
----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE OrderDetails
(
    OrderDetailId INT PRIMARY KEY IDENTITY(1,1),

    OrderId INT NOT NULL,

    ProductId INT NOT NULL,

    Quantity INT NOT NULL,

    UnitPrice DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_OrderDetail_Order
        FOREIGN KEY(OrderId)
        REFERENCES Orders(OrderId)
        ON DELETE CASCADE,

    CONSTRAINT FK_OrderDetail_Product
        FOREIGN KEY(ProductId)
        REFERENCES Products(ProductId)
);
----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Stock
(
    StockId INT PRIMARY KEY IDENTITY(1,1),

    ProductId INT NOT NULL UNIQUE,

    Quantity INT NOT NULL,

    LastUpdated DATETIME2 DEFAULT GETDATE(),

    CONSTRAINT FK_Stock_Product
        FOREIGN KEY(ProductId)
        REFERENCES Products(ProductId)
        ON DELETE CASCADE
);
----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Cart
(
    CartId INT PRIMARY KEY IDENTITY,
    UserId INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE CartItems
(
    CartItemId INT PRIMARY KEY IDENTITY,
    CartId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,

    FOREIGN KEY (CartId) REFERENCES Cart(CartId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
----------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE Wishlist
(
    WishlistId INT PRIMARY KEY IDENTITY,
    UserId INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
CREATE TABLE WishlistItems
(
    WishlistItemId INT PRIMARY KEY IDENTITY,
    WishlistId INT NOT NULL,
    ProductId INT NOT NULL,

    FOREIGN KEY (WishlistId) REFERENCES Wishlist(WishlistId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);


CREATE TYPE OrderItemType AS TABLE
(
    ProductId INT,
    Quantity INT,
    UnitPrice DECIMAL(10,2),
    Discount DECIMAL(5,2)
);
----------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Categories (CategoryName)
VALUES
('Designer'),
('Luxury Perfumes'),
('Arabic Perfumes'),
('Everyday Perfumes'),
('Niche Perfumes');
----------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Brands (BrandName)
VALUES
('Dior'),
('Chanel'),
('Lattafa'),
('Armaf'),
('Versace'),
('Creed'),
('Tom Ford'),
('Maison Francis Kurkdjian'),
('Guerlain'),
('YSL');
----------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Products
(
    ProductName,
    CategoryId,
    BrandId,
    Description,
    VolumeMl,
    Gender,
    Price,
    Discount
)
VALUES
(
'Dior Sauvage EDP',
1,
1,
'Fresh citrus fragrance',
100,
'Male',
12999,
10
),

(
'Chanel No.5',
2,
2,
'Elegant floral perfume',
50,
'Female',
15999,
5
),

(
'Lattafa Khamrah',
3,
3,
'Warm oriental fragrance',
100,
'Unisex',
3999,
15
),

(
'Armaf Club De Nuit',
4,
4,
'Long lasting everyday perfume',
105,
'Male',
4999,
8
),

(
'Versace Dylan Blue',
2,
5,
'Luxury masculine scent',
100,
'Male',
8999,
12
);
----------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Stock
(
ProductId,
Quantity
)
VALUES
(1,100),
(2,50),
(3,75),
(4,120),
(5,60);
----------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Users
(
Username,
Email,
PasswordHash,
Role,
IsVerified
)
VALUES
(
'Akhil',
'akhil@gmail.com',
'SampleHash',
'User',
1
),

(
'John',
'john@gmail.com',
'SampleHash',
'User',
1
),

(
'Sarah',
'sarah@gmail.com',
'SampleHash',
'User',
1
);
----------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Reviews
(
ProductId,
UserId,
Rating,
ReviewText
)
VALUES

(1,1,5,'Amazing fragrance'),
(1,2,5,'Best perfume'),

(2,1,4,'Very elegant'),

(3,2,3,'Good perfume'),

(4,3,5,'Excellent'),

(5,1,1,'Not my taste');
----------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Images(ProductId, ImageUrl, ImagePublicId)
VALUES
(1, 'https://res.cloudinary.com/demo/image/upload/dior-sauvage-edp.jpg', 'dior-sauvage-edp'),
(2, 'https://res.cloudinary.com/demo/image/upload/chanel-no5.jpg', 'chanel-no5'),
(3, 'https://res.cloudinary.com/demo/image/upload/lattafa-khamrah.jpg', 'lattafa-khamrah'),
(4, 'https://res.cloudinary.com/demo/image/upload/armaf-club-de-nuit.jpg', 'armaf-club-de-nuit'),
(5, 'https://res.cloudinary.com/demo/image/upload/versace-dylan-blue.jpg', 'versace-dylan-blue');

SELECT * FROM Categories;
SELECT * FROM Brands;
SELECT * FROM Products;
SELECT * FROM Stock;
SELECT * FROM Users;
SELECT * FROM Reviews;

SELECT * FROM Images;
SELECT *
sp_helptext 'sp_AddProduct'

    UPDATE Images
    SET ImageUrl = 'https://res.cloudinary.com/vkxpmnk0/image/upload/v1785753012/perfume_p4ntol.jpg',
        ImagePublicId = 'perfume_p4ntol'
    WHERE ImageId = 4;

EXEC sp_GetAllProducts
----------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------
GO

CREATE OR ALTER PROCEDURE sp_AddCategory
(
    @CategoryName VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Categories(CategoryName)
    VALUES(@CategoryName);

    SELECT SCOPE_IDENTITY() AS CategoryId;
END
GO
----------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetAllCategories
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CategoryId,
        CategoryName
    FROM Categories
    ORDER BY CategoryName;
END
GO
----------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_GetCategoryById
(
    @CategoryId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CategoryId,
        CategoryName
    FROM Categories
    WHERE CategoryId=@CategoryId;
END
GO
----------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_UpdateCategory
(
    @CategoryId INT,
    @CategoryName VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Categories
    SET
        CategoryName=@CategoryName
    WHERE CategoryId=@CategoryId;

    SELECT @@ROWCOUNT;
END
GO
----------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_DeleteCategory
(
    @CategoryId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Categories
    WHERE CategoryId=@CategoryId;

    SELECT @@ROWCOUNT;
END
GO
----------------------------------------------------------------------------------------------------------------------------------
EXEC sp_GetAllCategories;

EXEC sp_GetCategoryById 1;

EXEC sp_AddCategory
    'Summer Collection';

EXEC sp_UpdateCategory
    1,
    'Designer Perfumes';

EXEC sp_DeleteCategory 6;
----------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------------
/*=========================================================
                    BRAND
=========================================================*/
GO

CREATE OR ALTER PROCEDURE sp_AddBrand
(
    @BrandName VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Brands(BrandName)
    VALUES(@BrandName);

    SELECT SCOPE_IDENTITY();
END
GO

CREATE OR ALTER PROCEDURE sp_GetAllBrands
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        BrandId,
        BrandName
    FROM Brands
    ORDER BY BrandName;
END
GO

CREATE OR ALTER PROCEDURE sp_GetBrandById
(
    @BrandId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        BrandId,
        BrandName
    FROM Brands
    WHERE BrandId=@BrandId;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateBrand
(
    @BrandId INT,
    @BrandName VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Brands
    SET BrandName=@BrandName
    WHERE BrandId=@BrandId;

    SELECT @@ROWCOUNT;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteBrand
(
    @BrandId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Brands
    WHERE BrandId=@BrandId;

    SELECT @@ROWCOUNT;
END
GO


/*=========================================================
                    STOCK
=========================================================*/

CREATE OR ALTER PROCEDURE sp_AddStock
(
    @ProductId INT,
    @Quantity INT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Stock(ProductId,Quantity)
    VALUES(@ProductId,@Quantity);

    SELECT SCOPE_IDENTITY();
END
GO

CREATE OR ALTER PROCEDURE sp_GetAllStock
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        s.StockId,
        s.ProductId,
        p.ProductName,
        s.Quantity,
        s.LastUpdated
    FROM Stock s
    INNER JOIN Products p
        ON s.ProductId=p.ProductId
    ORDER BY p.ProductName;
END
GO

CREATE OR ALTER PROCEDURE sp_GetStockByProductId
(
    @ProductId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        s.StockId,
        s.ProductId,
        p.ProductName,
        s.Quantity,
        s.LastUpdated
    FROM Stock s
    INNER JOIN Products p
        ON s.ProductId=p.ProductId
    WHERE s.ProductId=@ProductId;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateStock
(
    @ProductId INT,
    @Quantity INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Stock
    SET
        Quantity=@Quantity,
        LastUpdated=GETDATE()
    WHERE ProductId=@ProductId;

    SELECT @@ROWCOUNT;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteStock
(
    @ProductId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Stock
    WHERE ProductId=@ProductId;

    SELECT @@ROWCOUNT;
END
GO


/*=========================================================
                    IMAGES
=========================================================*/

CREATE OR ALTER PROCEDURE sp_AddImage
(
    @ProductId INT,
    @ImageUrl VARCHAR(MAX),
    @ImagePublicId VARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Images
    (
        ProductId,
        ImageUrl,
        ImagePublicId
    )
    VALUES
    (
        @ProductId,
        @ImageUrl,
        @ImagePublicId
    );

    SELECT SCOPE_IDENTITY();
END
GO

CREATE OR ALTER PROCEDURE sp_GetAllImages
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ImageId,
        ProductId,
        ImageUrl,
        ImagePublicId
    FROM Images;
END
GO

CREATE OR ALTER PROCEDURE sp_GetImageById
(
    @ImageId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ImageId,
        ProductId,
        ImageUrl,
        ImagePublicId
    FROM Images
    WHERE ImageId=@ImageId;
END
GO

CREATE OR ALTER PROCEDURE sp_GetImageByProductId
(
    @ProductId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ImageId,
        ProductId,
        ImageUrl,
        ImagePublicId
    FROM Images
    WHERE ProductId=@ProductId;
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateImage
(
    @ImageId INT,
    @ProductId INT,
    @ImageUrl VARCHAR(MAX),
    @ImagePublicId VARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Images
    SET
        ProductId=@ProductId,
        ImageUrl=@ImageUrl,
        ImagePublicId=@ImagePublicId
    WHERE ImageId=@ImageId;

    SELECT @@ROWCOUNT;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteImage
(
    @ImageId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Images
    WHERE ImageId=@ImageId;

    SELECT @@ROWCOUNT;
END
GO
/*=========================================================
                    PRODUCTS
=========================================================*/
CREATE OR ALTER PROCEDURE sp_AddProduct
(
    @ProductName VARCHAR(255),
    @CategoryId INT,
    @BrandId INT,
    @Description VARCHAR(MAX),
    @VolumeMl INT,
    @Gender VARCHAR(20),
    @Price DECIMAL(10,2),
    @Discount DECIMAL(5,2)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Products
    (
        ProductName,
        CategoryId,
        BrandId,
        Description,
        VolumeMl,
        Gender,
        Price,
        Discount
    )
    VALUES
    (
        @ProductName,
        @CategoryId,
        @BrandId,
        @Description,
        @VolumeMl,
        @Gender,
        @Price,
        @Discount
    );

    SELECT SCOPE_IDENTITY();
END
GO
/*=========================================================*/ 
EXEC sp_SearchProducts 'v'
CREATE OR ALTER PROCEDURE sp_GetAllProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.ProductId,
        p.ProductName,
        c.CategoryName,
        b.BrandName,
        p.Description,
        p.VolumeMl,

        ISNULL(
        (
            SELECT AVG(CAST(Rating AS FLOAT))
            FROM Reviews r
            WHERE r.ProductId = p.ProductId
        ), 0) AS Rating,

        p.Gender,
        p.Price,
        p.Discount,
        p.CreatedAt,
        p.Updated,

        i.ImageId,
        i.ImageUrl,
        i.ImagePublicId

    FROM Products p

    INNER JOIN Categories c
        ON p.CategoryId = c.CategoryId

    INNER JOIN Brands b
        ON p.BrandId = b.BrandId

    LEFT JOIN Images i
        ON p.ProductId = i.ProductId

    ORDER BY p.ProductName;
END
GO

/*=========================================================*/

CREATE OR ALTER PROCEDURE sp_GetProductById
(
    @ProductId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.ProductId,
        p.ProductName,
        c.CategoryName,
        b.BrandName,
        p.Description,
        p.VolumeMl,

        ISNULL(
        (
            SELECT AVG(CAST(Rating AS FLOAT))
            FROM Reviews r
            WHERE r.ProductId = p.ProductId
        ),0) AS Rating,

        p.Gender,
        p.Price,
        p.Discount,
        p.CreatedAt,
        p.Updated,

        i.ImageId,
        i.ProductId,
        '' AS ProductName,
        i.ImageUrl,
        i.ImagePublicId

    FROM Products p
    INNER JOIN Categories c
        ON p.CategoryId = c.CategoryId
    INNER JOIN Brands b
        ON p.BrandId = b.BrandId
    LEFT JOIN Images i
        ON p.ProductId = i.ProductId

    WHERE p.ProductId = @ProductId;
END
GO

/*=========================================================*/

CREATE OR ALTER PROCEDURE sp_UpdateProduct
(
    @ProductId INT,
    @ProductName VARCHAR(255),
    @CategoryId INT,
    @BrandId INT,
    @Description VARCHAR(MAX),
    @VolumeMl INT,
    @Gender VARCHAR(20),
    @Price DECIMAL(10,2),
    @Discount DECIMAL(5,2)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Products
    SET

        ProductName=@ProductName,
        CategoryId=@CategoryId,
        BrandId=@BrandId,
        Description=@Description,
        VolumeMl=@VolumeMl,
        Gender=@Gender,
        Price=@Price,
        Discount=@Discount,
        Updated=GETDATE()

    WHERE ProductId=@ProductId;

    SELECT @@ROWCOUNT;
END
GO

/*=========================================================*/

CREATE OR ALTER PROCEDURE sp_DeleteProduct
(
    @ProductId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Products
    WHERE ProductId=@ProductId;

    SELECT @@ROWCOUNT;
END
GO

/*=========================================================*/
CREATE OR ALTER PROCEDURE sp_SearchProducts
(
    @Keyword VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.ProductId,
        p.ProductName,
        c.CategoryName,
        b.BrandName,
        p.Description,
        p.VolumeMl,

        ISNULL(
        (
            SELECT AVG(CAST(Rating AS FLOAT))
            FROM Reviews r
            WHERE r.ProductId = p.ProductId
        ),0) AS Rating,

        p.Gender,
        p.Price,
        p.Discount,
        p.CreatedAt,
        p.Updated,

        i.ImageId,
        i.ProductId,
        i.ImageUrl,
        i.ImagePublicId

    FROM Products p

    INNER JOIN Categories c
        ON p.CategoryId = c.CategoryId

    INNER JOIN Brands b
        ON p.BrandId = b.BrandId

    LEFT JOIN Images i
        ON p.ProductId = i.ProductId

    WHERE
        p.ProductName LIKE '%' + @Keyword + '%'
        OR c.CategoryName LIKE '%' + @Keyword + '%'
        OR b.BrandName LIKE '%' + @Keyword + '%'

    ORDER BY p.ProductName;
END
GO
/*=========================================================
                    REVIEWS
=========================================================*/

CREATE OR ALTER PROCEDURE sp_AddReview
(
    @ProductId INT,
    @UserId INT,
    @Rating INT,
    @ReviewText VARCHAR(1000)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Reviews
    (
        ProductId,
        UserId,
        Rating,
        ReviewText
    )
    VALUES
    (
        @ProductId,
        @UserId,
        @Rating,
        @ReviewText
    );

    SELECT SCOPE_IDENTITY();
END
GO

/*=========================================================*/

CREATE OR ALTER PROCEDURE sp_GetAllReviews
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        r.ReviewId,
        r.ProductId,
        p.ProductName,
        r.UserId,
        u.UserName,
        r.Rating,
        r.ReviewText,
        r.ReviewDate,
        r.UpdatedAt

    FROM Reviews r

    INNER JOIN Products p
        ON r.ProductId = p.ProductId

    INNER JOIN Users u
        ON r.UserId = u.UserId

    ORDER BY r.ReviewDate DESC;
END
GO

/*=========================================================*/

CREATE OR ALTER PROCEDURE sp_GetReviewById
(
    @ReviewId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        r.ReviewId,
        r.ProductId,
        p.ProductName,
        r.UserId,
        u.UserName,
        r.Rating,
        r.ReviewText,
        r.ReviewDate,
        r.UpdatedAt

    FROM Reviews r

    INNER JOIN Products p
        ON r.ProductId = p.ProductId

    INNER JOIN Users u
        ON r.UserId = u.UserId

    WHERE r.ReviewId=@ReviewId;
END
GO

/*=========================================================*/

CREATE OR ALTER PROCEDURE sp_GetReviewsByProductId
    @ProductId INT
AS
BEGIN
    SELECT
        r.ReviewId,
        r.ProductId,
        p.ProductName,
        r.UserId,
        u.Username AS UserName,
        r.Rating,
        r.ReviewText,
        r.ReviewDate,
        r.UpdatedAt
    FROM Reviews r
    INNER JOIN Users u ON r.UserId = u.UserId
    INNER JOIN Products p ON r.ProductId = p.ProductId
    WHERE r.ProductId = @ProductId
    ORDER BY r.ReviewDate DESC;
END

/*=========================================================*/
go
CREATE OR ALTER PROCEDURE sp_UpdateReview
(
    @ReviewId INT,
    @Rating INT,
    @ReviewText VARCHAR(1000)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Reviews
    SET
        Rating=@Rating,
        ReviewText=@ReviewText,
        UpdatedAt=GETDATE()

    WHERE ReviewId=@ReviewId;

    SELECT @@ROWCOUNT;
END
GO

/*=========================================================*/

CREATE OR ALTER PROCEDURE sp_DeleteReview
(
    @ReviewId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Reviews
    WHERE ReviewId=@ReviewId;

    SELECT @@ROWCOUNT;
END
GO
/*=========================================================
                    ORDERS
=========================================================*/
ALTER PROCEDURE sp_AddOrder
(
    @UserId INT,
    @TotalAmount DECIMAL(10,2),
    @ShippingAddress NVARCHAR(500)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Orders
    (
        UserId,
        TotalAmount,
        ShippingAddress,
        OrderDate,
        Status
    )
    VALUES
    (
        @UserId,
        @TotalAmount,
        @ShippingAddress,
        GETDATE(),
        'Pending'
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT);
END;
/*=========================================================*/
go
CREATE OR ALTER PROCEDURE sp_AddOrderDetail
(
    @OrderId INT,
    @ProductId INT,
    @Quantity INT,
    @UnitPrice DECIMAL(10,2)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO OrderDetails
    (
        OrderId,
        ProductId,
        Quantity,
        UnitPrice
    )
    VALUES
    (
        @OrderId,
        @ProductId,
        @Quantity,
        @UnitPrice
    );

    UPDATE Stock
    SET
        Quantity = Quantity - @Quantity,
        LastUpdated = GETDATE()
    WHERE ProductId=@ProductId;
END
GO

/*=========================================================*/
CREATE OR ALTER PROCEDURE sp_GetAllOrders
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        o.OrderId,
        o.UserId,
        u.UserName,
        u.Email,
        o.OrderDate,
        o.TotalAmount,
        o.ShippingAddress,
        o.Status
    FROM Orders o
    INNER JOIN Users u
        ON o.UserId = u.UserId
    ORDER BY o.OrderDate DESC;
END
GO

/*=========================================================*/

CREATE OR ALTER PROCEDURE sp_GetOrderById
(
    @OrderId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        od.OrderDetailId,
        od.OrderId,
        p.ProductName,
        od.Quantity,
        od.UnitPrice,
        od.Quantity * od.UnitPrice AS SubTotal

    FROM OrderDetails od

    INNER JOIN Products p
        ON od.ProductId=p.ProductId

    WHERE od.OrderId=@OrderId;
END
GO

/*=========================================================*/

CREATE OR ALTER PROCEDURE sp_GetOrdersByUserId
(
    @UserId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        OrderId,
        OrderDate,
        TotalAmount,
        Status

    FROM Orders

    WHERE UserId=@UserId

    ORDER BY OrderDate DESC;
END
GO

/*=========================================================*/

CREATE OR ALTER PROCEDURE sp_UpdateOrderStatus
(
    @OrderId INT,
    @Status VARCHAR(50)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Orders
    SET Status=@Status
    WHERE OrderId=@OrderId;

    SELECT @@ROWCOUNT;
END
GO

/*=========================================================*/

CREATE OR ALTER PROCEDURE sp_DeleteOrder
(
    @OrderId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM OrderDetails
    WHERE OrderId=@OrderId;

    DELETE FROM Orders
    WHERE OrderId=@OrderId;

    SELECT @@ROWCOUNT;
END
GO

CREATE OR ALTER PROCEDURE sp_GetMyOrders
(
    @UserId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        o.OrderId,
        o.UserId,
        o.OrderDate,
        o.TotalAmount,
        o.Status,
        o.ShippingAddress
    FROM Orders o
    WHERE o.UserId = @UserId
    ORDER BY o.OrderDate DESC;
END
GO
CREATE OR ALTER PROCEDURE sp_CancelOrder
(
    @OrderId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Orders
    SET Status = 'Cancelled'
    WHERE OrderId = @OrderId
      AND Status = 'Pending';

    SELECT @@ROWCOUNT;
END
GO
/*=========================================================
                REGISTER USER
=========================================================*/
CREATE OR ALTER PROCEDURE sp_RegisterUser
(
    @UserName VARCHAR(100),
    @Email VARCHAR(255),
    @PasswordHash VARCHAR(MAX),
    @LoginProvider VARCHAR(20),
    @IsVerified BIT,
    @VerificationCode VARCHAR(100) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Users
    (
        Username,
        Email,
        PasswordHash,
        LoginProvider,
        IsVerified,
        VerificationCode,
        CreatedAt
    )
    VALUES
    (
        @UserName,
        @Email,
        @PasswordHash,
        @LoginProvider,
        @IsVerified,
        @VerificationCode,
        GETDATE()
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT);
END;
GO
/*=========================================================
                LOGIN USER
=========================================================*/
CREATE OR ALTER PROCEDURE sp_LoginUser
(
    @Email VARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        UserId,
        UserName,
        Email,
        PasswordHash,
        Role,
        LoginProvider,
        IsVerified,
        VerificationCode,
        GoogleId,
        FacebookId
    FROM Users
    WHERE Email = @Email;
END
GO
/*=========================================================
                GET USER BY ID
=========================================================*/
CREATE OR ALTER PROCEDURE sp_GetUserById
(
    @UserId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        UserId,
        UserName,
        Email,
        Role,
        IsVerified,
        VerificationCode,
        GoogleId,
        FacebookId,
        CreatedAt,
        UpdatedAt
    FROM Users
    WHERE UserId=@UserId;
END
GO

/*=========================================================
                GET ALL USERS
=========================================================*/
CREATE OR ALTER PROCEDURE sp_GetAllUsers
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        UserId,
        UserName,
        Email,
        Role,
        IsVerified,
        GoogleId,
        FacebookId,
        CreatedAt,
        UpdatedAt
    FROM Users
    ORDER BY UserName;
END
GO

/*=========================================================
                UPDATE USER
=========================================================*/

CREATE OR ALTER PROCEDURE sp_UpdateUser
(
    @UserId INT,
    @UserName VARCHAR(100),
    @Email VARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET
        UserName=@UserName,
        Email=@Email,
        UpdatedAt=GETDATE()
    WHERE UserId=@UserId;

    SELECT @@ROWCOUNT;
END
GO

/*=========================================================
                DELETE USER
=========================================================*/

select * from users;
DELETE FROM Users
WHERE UserId = 6;

CREATE OR ALTER PROCEDURE sp_DeleteUser
(
    @UserId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Users
    WHERE UserId=@UserId;

    SELECT @@ROWCOUNT;
END
GO
--------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------------------------

/*=========================================================
                DASHBOARD
=========================================================*/

CREATE OR ALTER PROCEDURE sp_GetDashboardStats
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        (SELECT COUNT(*) FROM Products) AS TotalProducts,

        (SELECT COUNT(*) FROM Users) AS TotalUsers,

        (SELECT COUNT(*) FROM Orders) AS TotalOrders,

        (SELECT COUNT(*) FROM Reviews) AS TotalReviews,

        (SELECT SUM(Quantity) FROM Stock) AS TotalStock;
END
GO


/*=========================================================
                LOW STOCK
=========================================================*/

CREATE OR ALTER PROCEDURE sp_GetLowStockProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.ProductId,
        p.ProductName,
        s.Quantity

    FROM Products p

    INNER JOIN Stock s
        ON p.ProductId=s.ProductId

    WHERE s.Quantity<=10

    ORDER BY s.Quantity;
END
GO


/*=========================================================
                TOP RATED PRODUCTS
=========================================================*/

CREATE OR ALTER PROCEDURE sp_GetTopRatedProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.ProductId,
        p.ProductName,

        AVG(CAST(r.Rating AS FLOAT)) AS Rating

    FROM Products p

    INNER JOIN Reviews r
        ON p.ProductId=r.ProductId

    GROUP BY
        p.ProductId,
        p.ProductName

    ORDER BY Rating DESC;
END
GO


/*=========================================================
                BEST SELLERS
=========================================================*/

CREATE OR ALTER PROCEDURE sp_GetBestSellingProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT

        p.ProductId,

        p.ProductName,

        SUM(od.Quantity) AS TotalSold

    FROM Products p

    INNER JOIN OrderDetails od
        ON p.ProductId=od.ProductId

    GROUP BY

        p.ProductId,

        p.ProductName

    ORDER BY TotalSold DESC;
END
GO


/*=========================================================
                LATEST PRODUCTS
=========================================================*/

CREATE OR ALTER PROCEDURE sp_GetLatestProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 10

        p.ProductId,

        p.ProductName,

        c.CategoryName,

        b.BrandName,

        p.Price,

        (
            SELECT TOP 1 ImageUrl
            FROM Images i
            WHERE i.ProductId=p.ProductId
        ) AS ImageUrl

    FROM Products p

    INNER JOIN Categories c
        ON p.CategoryId=c.CategoryId

    INNER JOIN Brands b
        ON p.BrandId=b.BrandId

    ORDER BY p.CreatedAt DESC;
END
GO
----------------------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_AddToCart
(
    @UserId INT,
    @ProductId INT,
    @Quantity INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CartId INT;

    SELECT @CartId = CartId
    FROM Cart
    WHERE UserId = @UserId;

    IF @CartId IS NULL
    BEGIN
        INSERT INTO Cart(UserId)
        VALUES(@UserId);

        SET @CartId = SCOPE_IDENTITY();
    END

    IF EXISTS
    (
        SELECT 1
        FROM CartItems
        WHERE CartId = @CartId
        AND ProductId = @ProductId
    )
    BEGIN
        UPDATE CartItems
        SET Quantity = Quantity + @Quantity
        WHERE CartId = @CartId
        AND ProductId = @ProductId;
    END
    ELSE
    BEGIN
        INSERT INTO CartItems
        (
            CartId,
            ProductId,
            Quantity
        )
        VALUES
        (
            @CartId,
            @ProductId,
            @Quantity
        );
    END
END
GO

CREATE OR ALTER PROCEDURE sp_GetCart
(
    @UserId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ci.CartItemId,
        p.ProductId,
        p.ProductName,
        p.Price,
        p.Discount,
        ci.Quantity,

        (
            SELECT TOP 1 ImageUrl
            FROM Images
            WHERE ProductId = p.ProductId
            ORDER BY ImageId
        ) AS ImageUrl

    FROM Cart c
    INNER JOIN CartItems ci
        ON c.CartId = ci.CartId
    INNER JOIN Products p
        ON ci.ProductId = p.ProductId
    WHERE c.UserId = @UserId;
END
GO


CREATE OR ALTER PROCEDURE sp_UpdateCartQuantity
(
    @CartItemId INT,
    @Quantity INT
)
AS
BEGIN
    UPDATE CartItems
    SET Quantity = @Quantity
    WHERE CartItemId = @CartItemId;
END
GO

CREATE OR ALTER PROCEDURE sp_RemoveCartItem
(
    @CartItemId INT
)
AS
BEGIN
    DELETE FROM CartItems
    WHERE CartItemId = @CartItemId;
END
GO


CREATE OR ALTER PROCEDURE sp_ClearCart
(
    @UserId INT
)
AS
BEGIN
    DELETE ci
    FROM CartItems ci
    INNER JOIN Cart c
        ON ci.CartId = c.CartId
    WHERE c.UserId = @UserId;
END
GO
---------------------------------------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_AddToWishlist
(
    @UserId INT,
    @ProductId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @WishlistId INT;

    SELECT @WishlistId = WishlistId
    FROM Wishlist
    WHERE UserId = @UserId;

    IF @WishlistId IS NULL
    BEGIN
        INSERT INTO Wishlist(UserId)
        VALUES(@UserId);

        SET @WishlistId = SCOPE_IDENTITY();
    END

    IF NOT EXISTS
    (
        SELECT 1
        FROM WishlistItems
        WHERE WishlistId = @WishlistId
        AND ProductId = @ProductId
    )
    BEGIN
        INSERT INTO WishlistItems
        (
            WishlistId,
            ProductId
        )
        VALUES
        (
            @WishlistId,
            @ProductId
        );
    END
END
GO
CREATE OR ALTER PROCEDURE sp_GetWishlist
(
    @UserId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        wi.WishlistItemId,
        p.ProductId,
        p.ProductName,
        p.Price,
        p.Discount,

        i.ImageId,
        i.ImageUrl

    FROM Wishlist w
    INNER JOIN WishlistItems wi
        ON w.WishlistId = wi.WishlistId

    INNER JOIN Products p
        ON wi.ProductId = p.ProductId

    LEFT JOIN Images i
        ON p.ProductId = i.ProductId

    WHERE w.UserId = @UserId;
END
GO


CREATE OR ALTER PROCEDURE sp_RemoveWishlistItem
(
    @WishlistItemId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM WishlistItems
    WHERE WishlistItemId = @WishlistItemId;
END
GO

CREATE OR ALTER PROCEDURE sp_ClearWishlist
(
    @UserId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE wi
    FROM WishlistItems wi
    INNER JOIN Wishlist w
        ON wi.WishlistId = w.WishlistId
    WHERE w.UserId = @UserId;
END
GO
---------------------------------------------------------------------------------------------------------------------------------------
