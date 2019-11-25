# Technologies Used
- Java Springboot 2.1.+
- Maven
- SQL Server
- REST
- Apache POI

# Database Setup
Test SQL Server database is hosted on cloud so no need to create any local database. 
You will have to provide following connection properties in src/main/resources/application.yaml:

 - spring.datasource.url
 - spring.datasource.username
 - spring.datasource.password
 
# Build and start the server

## running the executable jar
To build the jar: `mvn clean package`

Run with default application config (in project source):  `java -jar target/mudlog-service-backend-0.0.1-SNAPSHOT.jar`

Run with external config: `java -jar target/mudlog-service-backend-0.0.1-SNAPSHOT.jar --spring.config.location=file:///Users/home/config/applicatino.yaml`

## passing datasource config at command line
`java -jar target/mudlog-service-backend-0.0.1-SNAPSHOT.jar --spring.datasource.url=<url> --spring.datasource.username=sa --spring.datasource.password=<strong-password>`