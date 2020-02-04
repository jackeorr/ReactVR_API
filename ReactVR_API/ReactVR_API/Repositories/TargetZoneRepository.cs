﻿using Dapper;
using ReactVR_API.HelperClasses;
using ReactVR_CORE.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ReactVR_API.Repositories
{
    class TargetZoneRepository : BaseRepository
    {
        public Guid CreateTargetZone(TargetZone targetZone)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    targetZone.TargetZoneShape,
                    targetZone.TargetZoneX,
                    targetZone.TargetZoneY,
                    targetZone.TargetZoneZ
                };

                var sql = SqlCrudHelper.GetInsertStatement(parameters, targetZone.GetType().Name, "TargetZoneId");

                Guid newId = db.ExecuteScalar<Guid>(sql, parameters);

                return newId;
            }
        }

        public TargetZone GetTargetZoneById(Guid targetZoneId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var parameters = new { targetZoneId };
                var sql = "select * from targetZone where targetZoneid = @targetZoneId";

                var targetZone = db.QuerySingle<TargetZone>(sql, parameters);

                return targetZone;
            }
        }

        public void UpdateTargetZone(TargetZone targetZone)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    targetZone.TargetZoneShape,
                    targetZone.TargetZoneX,
                    targetZone.TargetZoneY,
                    targetZone.TargetZoneZ
                };

                var sql = SqlCrudHelper.GetUpdateStatement(parameters, targetZone.GetType().Name);
                sql += " WHERE TargetZoneId = @TargetZoneId";

                db.Execute(sql, targetZone);
            }
        }

        public void DeleteTargetZone(Guid targetZoneId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    TargetZoneId = targetZoneId
                };
                var sql = "update targetZone set [IsDeleted] = 1 WHERE [TargetZoneId] = @TargetZoneId";

                db.Execute(sql, parameters);
            }
        }
    }
}
