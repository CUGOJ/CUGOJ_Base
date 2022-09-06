#! /bin/bash

dotnet ef dbcontext scaffold "server=;port=4000;database=cugoj;user=cugoj;password=cugoj123456" Pomelo.EntityFrameworkCore.MySql --context-dir Dao/DB/Context --output-dir Dao/DB/Models --context CUGOJContext --namespace CUGOJ.Base.Dao.DB.Models --context-namespace CUGOJ.Base.Dao.DB.Context