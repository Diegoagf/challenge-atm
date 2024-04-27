INSERT INTO Users (Name, Rol, CreatedBy, CreatedAt, LastModifiedBy, LastModified)
VALUES 
    ('Juan Perez', 'Cliente', 'Admin', GETDATE(), 'Admin', GETDATE()),
    ('Maria Gonzalez', 'Administrador', 'Admin', GETDATE(), 'Admin', GETDATE());

INSERT INTO Cards (CardNumber, Pin, AccountNumber, IsBlocked, Balance, CreatedBy, CreatedAt, LastModifiedBy, LastModified)
VALUES 
    ('4970110000000062', 1234, '01865281110786583688', 0, 0, 'Admin', GETDATE(), 'Admin', GETDATE()),
    ('36230000000019', 0001, '31831767132372861697', 0, 0, 'Admin', GETDATE(), 'Admin', GETDATE());
