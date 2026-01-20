using System.Threading.Tasks;

using Niantic.Lightship.AR.LocationAR;
using Niantic.Lightship.AR.PersistentAnchors;
using Niantic.Lightship.AR.Subsystems;

using UnityEngine;

public class MeshDownloadHowTo : MonoBehaviour
{
  [SerializeField]
  private LocationMeshManager _meshManager;

  [SerializeField]
  private ARLocationManager _arLocationManager;

  private GameObject _downloadedMesh;
  private bool _startedDownload;

  private void Start()
  {
    _arLocationManager.locationTrackingStateChanged += OnLocationTrackingStateChanged;
  }

  private void OnLocationTrackingStateChanged(ARLocationTrackedEventArgs args)
  {
    if (args.Tracking && !_startedDownload)
    {
      _startedDownload = true;
      _ = DownloadAndPositionMeshAsync(location: args.ARLocation);
    }
  }

  private async Task DownloadAndPositionMeshAsync(ARLocation location)
  {
    var payload = location.Payload;

    // wait async for the mesh to download so it doesn't block the main thread
    var go = await _meshManager.GetLocationMeshForPayloadAsync(payload.ToBase64());

    // set the mesh as a child of the ARLocation's position and place it in the scene
    go.transform.SetParent(location.transform, false);
    _downloadedMesh = go;
  }

  private void OnDestroy()
  {
    if (_downloadedMesh)
    {
      Destroy(_downloadedMesh);
    }
  }
}