# minishop

### 描述

minishop 為一個模擬購物網站，會員系統有加入角色權限，使用 Session 實作購物車，將購物車內商品轉為訂單，後台管理，本專案使用 Repository patten。

### 開發環境

- Dotnet Core MVC 6.0
- SQL Service 2022
- Docker
- Azure Data Studio
- 作業環境 Mac

### 啟動專案開發

1. 建制映像檔

```
$ docker build -t minishop:v1 .
```

2. .env-example 重新命名.env 設置 MSSQL Password
3. 啟動 container

```
$ docker-compose --env-file .env up
```

4. 初始化秘密

```
$ cd src/minishop
$ dotnet user-secrets init
$ user secret set DefaultConnection:Password = A&VeryComplex123Password
```

5. 資料庫建立

```
$ dotnet ef database update
```

```
dotnet run
```

### Remark:

- 測試 admin:
  - username: admin123@admin.com
  - password: admin123
