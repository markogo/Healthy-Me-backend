using DAL=ee.itcollege.magord.healthyfood.DAL;

namespace PublicApi.DTO.v1.Mappers
{
    public abstract class BaseMapper<TLeftObject, TRightObject> : DAL.Base.Mappers.BaseMapper<TLeftObject, TRightObject>
        where TLeftObject : class?, new()
        where TRightObject : class?, new()
    {
    }
}