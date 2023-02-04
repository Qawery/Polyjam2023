using Polyjam2023;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DefaultInstaller", menuName = "Installers/DefaultInstaller")]
public class DefaultInstaller : ScriptableObjectInstaller<DefaultInstaller>
{
    [SerializeField] private CardLibrary cardLibrary;
    [SerializeField] private CardWidget cardWidgetPrefab;
    [SerializeField] private UnitWidget unitWidgetPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<CardLibrary>().FromScriptableObject(cardLibrary).AsCached();
        Container.Bind<CardWidget>().FromComponentInNewPrefab(cardWidgetPrefab).AsCached();
        Container.Bind<UnitWidget>().FromComponentInNewPrefab(unitWidgetPrefab).AsCached();
    }
}