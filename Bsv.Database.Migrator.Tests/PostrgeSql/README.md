# �������� ������� ������� �� PostgreSql
����� �������� ����������

- ���������� Docker
- ��������� ������� ��� ������ ���������� � ����� ������
```
docker run --name test-postgres-db -e POSTGRES_USER=net_user -e POSTGRES_PASSWORD=123456 -e POSTGRES_DB=postgres -p 5432:5432 -d postgres
