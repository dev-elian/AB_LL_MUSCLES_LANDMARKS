using System.Collections.Generic;
using UnityEngine;

public static class MuscleHighlightManager {
    static Dictionary<GameObject, (Renderer renderer, Material[] originalMaterials, int refs)> _highlightedObjects = new();

    public static void Highlight(GameObject obj, Material highlightMat) {
        if (!obj.TryGetComponent<Renderer>(out var renderer)) return;

        if (_highlightedObjects.ContainsKey(obj)) {
            _highlightedObjects[obj] = (renderer, _highlightedObjects[obj].originalMaterials, _highlightedObjects[obj].refs + 1);
            return;
        }

        var originalMaterials = renderer.materials;
        var highlightMaterials = new Material[originalMaterials.Length];
        for (int i = 0; i < highlightMaterials.Length; i++)
            highlightMaterials[i] = highlightMat;

        renderer.materials = highlightMaterials;
        _highlightedObjects[obj] = (renderer, originalMaterials, 1);
    }

    public static void Unhighlight(GameObject obj) {
        if (!_highlightedObjects.ContainsKey(obj)) return;

        var data = _highlightedObjects[obj];
        data.refs--;

        if (data.refs <= 0) {
            data.renderer.materials = data.originalMaterials;
            _highlightedObjects.Remove(obj);
        } else {
            _highlightedObjects[obj] = data;
        }
    }
}
