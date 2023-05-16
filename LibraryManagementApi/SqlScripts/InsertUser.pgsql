select table_schema, table_name from information_schema.tables where table_name = 'Users';


INSERT INTO public.Users(Username,Password,Role) VALUES
('admin', 'pass','Admin')
;
