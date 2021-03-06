﻿using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.LinkServices;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Initial;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    public class AddGameMaintenanceProcessor : IAddGameMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IGameLinkService _gameLinkService;
        private readonly IAddGameQueryProcessor _queryProcessor;

        public AddGameMaintenanceProcessor(
            IAutoMapper autoMapper,
            IGameLinkService gameLinkService,
            IAddGameQueryProcessor queryProcessor)
        {
            _autoMapper = autoMapper;
            _queryProcessor = queryProcessor;
            _gameLinkService = gameLinkService;
        }

        public Game AddGame(NewGame newGame)
        {
            // Map service model to entity model
            var gameEntity = _autoMapper.Map<Data.Entities.Game>(newGame);

            // Persist entity model
            _queryProcessor.AddGame(gameEntity);

            // Map new entity model back to full service model
            var game = _autoMapper.Map<Game>(gameEntity);

            // Add links to service model
            _gameLinkService.AddLinks(game);

            return game;
        }
    }
}