﻿using OutfitTrack.Application.ApiManagement;
using OutfitTrack.Domain.Entities;
using OutfitTrack.Domain.Interfaces;
using OutfitTrack.Application.Interfaces;

namespace OutfitTrack.Application.Services;

public class BaseService<TIBaseRepository, TInputCreate, TInputUpdate, TEntity, TOutput, TInputIdentifier>(IUnitOfWork? unitOfWork) : IBaseService<TInputCreate, TInputUpdate, TOutput, TInputIdentifier>
    where TIBaseRepository : IBaseRepository<TEntity, TInputIdentifier>
    where TEntity : BaseEntity<TEntity>, new()
    where TInputCreate : class
    where TInputUpdate : class
    where TOutput : class
    where TInputIdentifier : class
{
    protected Guid _guidApiDataRequest;
    protected readonly IUnitOfWork? _unitOfWork = unitOfWork;
    protected readonly TIBaseRepository? _repository = unitOfWork!.GetRepository<TIBaseRepository, TEntity, TInputIdentifier>();

    public void SetGuid(Guid guidApiDataRequest)
    {
        _guidApiDataRequest = guidApiDataRequest;
        GenericModule.SetGuidApiDataRequest(this, guidApiDataRequest);
    }

    #region Read
    public virtual IEnumerable<TOutput>? GetAll()
    {
        var listEntity = _repository!.GetAll();
        return listEntity != null ? FromEntityToOutput(listEntity) : null;
    }

    public virtual TOutput? Get(long id)
    {
        var entity = _repository!.Get(x => x.Id == id);
        return entity != null ? FromEntityToOutput(entity) : null;
    }

    public virtual TOutput? GetByIdentifier(TInputIdentifier inputIdentifier)
    {
        var entity = _repository!.GetByIdentifier(inputIdentifier);
        return entity != null ? FromEntityToOutput(entity) : null;
    }
    #endregion

    #region Create
    public virtual TOutput Create(TInputCreate inputCreate)
    {
        TEntity entity = _repository!.Create(FromInputCreateToEntity(inputCreate)) ?? throw new InvalidOperationException("Falha ao criar a entidade.");
        _unitOfWork!.Commit();
        return FromEntityToOutput(entity);
    }
    #endregion

    #region Update
    public virtual TOutput Update(long id, TInputUpdate inputUpdate)
    {
        var oldEntity = _repository!.Get(x => x.Id == id) ?? throw new KeyNotFoundException("Id inválido ou inexistente.");

        var updatedEntity = UpdateEntity(oldEntity, inputUpdate);
        var result = _repository!.Update(oldEntity) ?? throw new InvalidOperationException("Falha ao atualizar a entidade.");
        _unitOfWork!.Commit();

        return FromEntityToOutput(result);
    }

    protected static TEntity? UpdateEntity(TEntity oldEntity, TInputUpdate inputUpdate)
    {
        foreach (var property in typeof(TInputUpdate).GetProperties())
        {
            var correspondingProperty = typeof(TEntity).GetProperty(property.Name);
            if (correspondingProperty != null)
            {
                var value = property.GetValue(inputUpdate, null);

                correspondingProperty?.SetValue(oldEntity, value, null);
            }
        }
        return oldEntity;
    }
    #endregion

    #region Delete
    public virtual bool Delete(long id)
    {
        var entity = _repository!.Get(x => x.Id == id) ?? throw new KeyNotFoundException("Id inválido ou inexistente.");
        _repository.Delete(entity);
        _unitOfWork!.Commit();

        return true;
    }
    #endregion

    #region Mapper
    public TOutput FromEntityToOutput(TEntity entity)
    {
        return ApiData.Mapper!.MapperEntityOutput.Map<TEntity, TOutput>(entity);
    }

    public IEnumerable<TOutput> FromEntityToOutput(IEnumerable<TEntity> listEntity)
    {
        return ApiData.Mapper!.MapperEntityOutput.Map<IEnumerable<TEntity>, IEnumerable<TOutput>>(listEntity);
    }

    public TEntity FromOutputToEntity(TOutput output)
    {
        return ApiData.Mapper!.MapperEntityOutput.Map<TOutput, TEntity>(output);
    }

    public TEntity FromInputCreateToEntity(TInputCreate inputCreate)
    {
        return ApiData.Mapper!.MapperInputEntity.Map<TInputCreate, TEntity>(inputCreate);
    }

    public TOutputClass CustomMapper<TInputClass, TOutputClass>(TInputClass output)
    {
        return ApiData.Mapper!.MapperEntityOutput.Map<TInputClass, TOutputClass>(output);
    }
    #endregion
}