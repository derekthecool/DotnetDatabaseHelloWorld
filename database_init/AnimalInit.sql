-- Only should be run once upon creation of the container to initialize the database and tables
create database Animal;

use Animal;

create table Animals
(
    Id int not null auto_increment,
    Name varchar(100) not null,
    LegCount int unsigned not null,
    primary key (Id)
);

INSERT INTO Animals (Name, LegCount) VALUES  ('Epona', 4);
INSERT INTO Animals (Name, LegCount) VALUES  ('Flicker', 2);
INSERT INTO Animals (Name, LegCount) VALUES  ('Scooter the wheelchair pup', 2);
