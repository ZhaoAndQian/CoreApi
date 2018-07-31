using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Preoff.Repository;

namespace Preoff.Controllers
{
    /// <summary>
    /// 无人机控制器
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("Place")]
    [EnableCors("AllowAllOrigins")]
    public class PlaceController : BaseController
    {
        /// <summary>
        /// 无人机仓库
        /// </summary>
        public readonly IPlaceRepository _repository;
        ILog log = LogManager.GetLogger(Startup.Logrepository.Name, typeof(Startup));
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db">注入数据仓库</param>
        public PlaceController(IPlaceRepository _db)
        {
            _repository = _db;
        }

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetProvince")]
        public IActionResult GetProvince()
        {
            return Json(new
            {
                table = _repository.GetProvince(),
                status = '0',
                msg = "操作成功!"
            });
        }
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("PlaceType")]
        public IActionResult GetPlaceType()
        {
            return Json(new
            {
                table = _repository.GetPlaceType(),
                status = '0',
                msg = "操作成功!"
            });
        }
        /// <summary>
        /// 按省份和类型查询数据及范围
        /// </summary>
        /// <param name="Province">省份</param>
        /// <param name="PlaceType">类型</param>
        /// <returns></returns>
        [HttpGet("GetPlace")]
        public IActionResult GetPlace(string Province,string PlaceType)
        {
            return Json(new
            {
                table = _repository.GetPlace(Province, PlaceType),
                status = '0',
                msg = "操作成功!"
            });
        }
    }
}
