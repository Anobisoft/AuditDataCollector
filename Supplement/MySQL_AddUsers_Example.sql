
CREATE USER 'auditdc_lcl'@'localhost' IDENTIFIED BY 'userpassword';
GRANT DELETE, INSERT, SELECT, UPDATE ON auditdb.* TO 'auditdc_lcl'@'localhost';

CREATE USER 'auditdc'@'%' IDENTIFIED BY 'userpassword';
GRANT DELETE, INSERT, SELECT, UPDATE ON auditdb.* TO 'auditdc'@'%';

