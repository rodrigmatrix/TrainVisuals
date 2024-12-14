using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Colossal.Entities;
using Game.Common;
using Game.Prefabs;
using Game.Routes;
using Game.UI;
using Game.Vehicles;
using TrainVisuals.Code.Utils;
using Unity.Entities;
using Unity.Mathematics;

namespace TrainVisuals.Code.Formulas;

public static class TrainFormulas
{
    private static NameSystem _nameSystem;
    private static PrefabSystem _prefabSystem;
    private static EntityManager _entityManager;

    private static readonly string[] ViaMobilidadeLines = ["5", "8", "9", "17"];

    private static readonly StringDictionary  ModelsDictionary = new()
    {
        { "SubwayCar01", "A" },
        { "SubwayEngine01", "A" },
        { "EU_TrainPassengerCar01", "B" },
        { "EU_TrainPassengerEngine01", "B" },
        { "NA_TrainPassengerCar01", "C" },
        { "NA_TrainPassengerEngine01", "C" },
    };

    private const string LinhaUni = "6";

    private const string ViaQuatro = "4";

    private static readonly Func<Entity, string> GetSubwayOperatorImageBinding = (entityRef) =>
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _nameSystem ??= World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<NameSystem>();
        _prefabSystem ??= World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<PrefabSystem>();
        _entityManager.TryGetComponent<Controller>(entityRef, out var controller);
        _entityManager.TryGetComponent<CurrentRoute>(controller.m_Controller, out var currentRoute);
        _entityManager.TryGetComponent<RouteNumber>(currentRoute.m_Route, out var routeNumber);

        var lineName = _nameSystem.GetName(currentRoute.m_Route).Translate().Split(' ').LastOrDefault();
        var routeString = lineName is { Length: >= 1 and <= 2 } ? lineName : routeNumber.m_Number.ToString();
        
        if (ViaQuatro == routeString)
        {
            return "ViaQuatroWhite";
        }
        if (ViaMobilidadeLines.Contains(routeString))
        {
            return "ViaMobilidadeWhite";
        }

        if (LinhaUni == routeString)
        {
            return "LinhaUniWhite";
        }

        return "MetroWhite";

        // for the train model, set by prefab name
        // get all OwnedVehicles from map and get the index
    };
    
    private static readonly Func<Entity, string> GetTrainOperatorImageBinding = (entityRef) =>
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _nameSystem ??= World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<NameSystem>();
        _prefabSystem ??= World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<PrefabSystem>();
        _entityManager.TryGetComponent<Controller>(entityRef, out var controller);
        _entityManager.TryGetComponent<CurrentRoute>(controller.m_Controller, out var currentRoute);
        _entityManager.TryGetComponent<RouteNumber>(currentRoute.m_Route, out var routeNumber);

        var lineName = _nameSystem.GetName(currentRoute.m_Route).Translate().Split(' ').LastOrDefault();
        var routeString = lineName is { Length: >= 1 and <= 2 } ? lineName : routeNumber.m_Number.ToString();
        
        if (ViaQuatro == routeString)
        {
            return "ViaQuatroWhite";
        }
        if (ViaMobilidadeLines.Contains(routeString))
        {
            return "ViaMobilidadeWhite";
        }

        if (LinhaUni == routeString)
        {
            return "LinhaUniWhite";
        }

        return "CptmWhite";

        // for the train model, set by prefab name
        // get all OwnedVehicles from map and get the index
    };
    
    private static readonly Func<Entity, string> GetTrainNameBinding = (entityRef) =>
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _nameSystem ??= World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<NameSystem>();
        _entityManager.TryGetComponent<Controller>(entityRef, out var controller);
        _entityManager.TryGetComponent<Owner>(controller.m_Controller, out var owner);
        _entityManager.TryGetBuffer<LayoutElement>(controller.m_Controller, true, out var layoutElements);
        _entityManager.TryGetBuffer<OwnedVehicle>(owner.m_Owner, true, out var ownerVehicles);
        int index = 0;
        int carIndex = 0;
    
        for (int i = 0; i < ownerVehicles.Length; ++i)
        {
            if (ownerVehicles[i].m_Vehicle == controller.m_Controller)
            {
                index = i;
            }
        }
        
        for (int i = 0; i < layoutElements.Length; ++i)
        {
            if (layoutElements[i].m_Vehicle == entityRef)
            {
                carIndex = i;
            }
        }

        index++;
        carIndex++;
        var entityDebugName = _nameSystem.GetDebugName(entityRef);
        var entityName = entityDebugName.TrimEnd(' ').Remove(entityDebugName.LastIndexOf(' ') + 1);
        string letter = "F";
        if (ModelsDictionary.ContainsKey(entityName))
        {
            letter = ModelsDictionary[entityName];
        }

        if (entityName.Contains("Subway"))
        {
            letter = "A";
        }
        if (entityName.Contains("EU_Train"))
        {
            letter = "B";
        }
        if (entityName.Contains("NA_Train"))
        {
            letter = "C";
        }
        if (index < 10)
        {
            return letter + "0" + index + carIndex;
        }

        return letter + index + carIndex;
    };
    
    private static readonly Func<Entity, string> GetDestinationTitleBinding = (entityRef) =>
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _nameSystem ??= World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<NameSystem>();
        _entityManager.TryGetComponent<Controller>(entityRef, out var controller);
        _entityManager.TryGetBuffer<LayoutElement>(controller.m_Controller, true, out var layoutElements);
        var carIndex = 0;
        for (var i = 0; i < layoutElements.Length; i++)
        {
            if (layoutElements[i].m_Vehicle == entityRef)
            {
                carIndex = i;
            }
        }

        return "Destination"; 
        
        return " ";
    };
    
    private static readonly Func<Entity, string> GetDestinationBinding = (entityRef) =>
    {
        try
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _nameSystem ??= World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<NameSystem>();

            _entityManager.TryGetComponent<Controller>(entityRef, out var controller);
            _entityManager.TryGetBuffer<LayoutElement>(controller.m_Controller, true, out var layoutElements);
            
            // var carIndex = 0;
            // for (var i = 0; i < layoutElements.Length; i++)
            // {
            //     if (layoutElements[i].m_Vehicle == entityRef)
            //     {
            //         carIndex = i;
            //     }
            // }
            //
            // if (carIndex != 0)
            // {
            //     return " ";
            // }
            
            _entityManager.TryGetComponent<TrainNavigation>(controller.m_Controller, out var trainNavigation);
            _entityManager.TryGetComponent<CurrentRoute>(controller.m_Controller, out var currentRoute);
            _entityManager.TryGetBuffer<RouteWaypoint>(currentRoute.m_Route, true, out var routeWaypoints);
            var filteredWaypoints = new List<RouteWaypoint>();
            //var closestWaypoint = routeWaypoints[0];
            //var furthestWaypoint = routeWaypoints[0];
            var closestDistance = 0f;
            var furthestDistance = 0f;
            for (var i = 0; i < routeWaypoints.Length; i++)
            {
                if (_entityManager.HasComponent<Connected>(routeWaypoints[i].m_Waypoint))
                {
                    // if (_entityManager.TryGetComponent<Position>(routeWaypoints[i].m_Waypoint, out var waypointPosition))
                    // {
                    //     _entityManager.TryGetComponent<Position>(closestWaypoint.m_Waypoint, out var closestPosition);
                    //     _entityManager.TryGetComponent<Position>(furthestWaypoint.m_Waypoint, out var furthestPosition);
                    //     var distanceClosest = math.distance(closestPosition.m_Position, waypointPosition.m_Position);
                    //     var distanceFurthest = math.distance(furthestPosition.m_Position, waypointPosition.m_Position);
                    //     if (closestDistance < distanceClosest)
                    //     {
                    //         closestDistance = distanceClosest;
                    //         closestWaypoint = routeWaypoints[i];
                    //     }
                    //
                    //     if (distanceFurthest > furthestDistance)
                    //     {
                    //         furthestDistance = distanceFurthest;
                    //         furthestWaypoint = routeWaypoints[i];
                    //     }
                    // }
                    filteredWaypoints.Add(routeWaypoints[i]);
                }
            }
            // Mod.log.Info("Closest " +GetRouteBuildingName(closestWaypoint));
            // Mod.log.Info("Fursthest " + GetRouteBuildingName(furthestWaypoint));
            var distanceFront = math.distance(closestDistance, trainNavigation.m_Front.m_Position);
            var distanceBack = math.distance(closestDistance, trainNavigation.m_Rear.m_Position);
           
            if (distanceFront < distanceBack)
            {
                return GetRouteBuildingName(filteredWaypoints[0]);
            }

            return GetRouteBuildingName(filteredWaypoints[filteredWaypoints.Count - 1]);
        }
        catch (Exception e)
        {
            return "";
        }
    };

    private static string GetRouteBuildingName(RouteWaypoint routeWaypoint)
    {
        _entityManager.TryGetComponent<Connected>(routeWaypoint.m_Waypoint, out var connected);
        _entityManager.TryGetComponent<Owner>(connected.m_Connected, out var owner);
        if (_entityManager.TryGetComponent<Owner>(owner.m_Owner, out var buildingOwner))
        {
            return _nameSystem.GetName(buildingOwner.m_Owner).Translate();
        }
        return _nameSystem.GetName(owner.m_Owner).Translate();
    }
    
    public static string GetSubwayOperatorIcon(Entity entityRef) => 
        GetSubwayOperatorImageBinding?.Invoke(entityRef) + "Icon" ?? "Transparent";
    
    public static string GetSubwayOperatorLogo(Entity entityRef) => 
        GetSubwayOperatorImageBinding?.Invoke(entityRef) + "Logo" ?? "Transparent";
    
    public static string GetTrainOperatorIcon(Entity entityRef) => 
        GetTrainOperatorImageBinding?.Invoke(entityRef) + "Icon" ?? "Transparent";
    
    public static string GetTrainOperatorLogo(Entity entityRef) => 
        GetTrainOperatorImageBinding?.Invoke(entityRef) + "Logo" ?? "Transparent";
    
    public static string GetSubwayTrainName(Entity entityRef) => 
        GetTrainNameBinding?.Invoke(entityRef) ?? "Transparent";
    
    public static string GetDestination(Entity entityRef) => 
        GetDestinationBinding?.Invoke(entityRef) ?? " ";
    
    public static string GetDestinationTitle(Entity entityRef) => 
        GetDestinationTitleBinding?.Invoke(entityRef) ?? " ";
}