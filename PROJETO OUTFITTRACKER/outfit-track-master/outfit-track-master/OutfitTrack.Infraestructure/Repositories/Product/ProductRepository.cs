﻿using OutfitTrack.Arguments;
using OutfitTrack.Domain.Entities;
using OutfitTrack.Domain.Interfaces;

namespace OutfitTrack.Infraestructure.Repositories;

public class ProductRepository(OutfitTrackContext context) : BaseRepository<Product, InputIdentifierProduct>(context), IProductRepository { }