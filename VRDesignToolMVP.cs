using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using VRC.SDKBase;

public class VRDesignTool : MonoBehaviour
{
    // プリミティブの種類
    public enum PrimitiveType
    {
        Sphere, Cube, Cylinder
    }

    // ツール選択
    public VRCUiPage toolSelectionPage;
    // プリミティブの種類
    public PrimitiveType selectedPrimitive;

    // 現在の操作モード
    private enum Mode
    {
        Create, Move, Rotate, Scale
    }
    private Mode currentMode;

    // 作成中のオブジェクト
    private GameObject creatingObject;
    // 選択中のオブジェクト
    private GameObject selectedObject;

    // VRコントローラーのイベントを受け取る
    private XRBaseInteractor interactor;

    void Start()
    {
        // VRコントローラーのイベントを取得
        interactor = GetComponent<XRBaseInteractor>();
        interactor.selectEntered.AddListener(OnSelectEnter);
        interactor.selectExited.AddListener(OnSelectExit);

        // ツール選択イベント
        toolSelectionPage.OnUiElementValueChanged += OnToolChange;
    }

    void Update()
    {
        // VRコントローラーの入力に応じた処理
        switch (currentMode)
        {
            case Mode.Create:
                // トリガーを押すと、プリミティブを作成
                if (interactor.selectTarget != null && interactor.selectTarget.TryGetComponent(out XRBaseInteractable interactable))
                {
                    if (interactor.isSelectActive)
                    {
                        CreatePrimitive(interactable.transform.position);
                        interactor.selectTarget = null;
                    }
                }
                break;

            case Mode.Move:
                // オブジェクト移動
                if (selectedObject != null && interactor.isSelectActive)
                {
                    selectedObject.transform.position = interactor.transform.position;
                }
                break;

            case Mode.Rotate:
                // オブジェクト回転
                if (selectedObject != null && interactor.isSelectActive)
                {
                    // ... (回転処理の実装)
                }
                break;

            case Mode.Scale:
                // オブジェクトスケール
                if (selectedObject != null && interactor.isSelectActive)
                {
                    // ... (スケール処理の実装)
                }
                break;
        }
    }

    // ツール選択イベント
    private void OnToolChange(VRCUiPage page, VRCUiElement element)
    {
        // 選択されたツールに応じてモードを変更
        switch (element.name)
        {
            case "Create":
                currentMode = Mode.Create;
                break;

            case "Move":
                currentMode = Mode.Move;
                break;

            case "Rotate":
                currentMode = Mode.Rotate;
                break;

            case "Scale":
                currentMode = Mode.Scale;
                break;
        }
    }

    // オブジェクト選択開始
    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        if (currentMode == Mode.Move || currentMode == Mode.Rotate || currentMode == Mode.Scale)
        {
            selectedObject = args.interactable.gameObject;
        }
    }

    // オブジェクト選択解除
    private void OnSelectExit(SelectExitEventArgs args)
    {
        // ...
    }

    // プリミティブ作成
    private void CreatePrimitive(Vector3 position)
    {
        switch (selectedPrimitive)
        {
            case PrimitiveType.Sphere:
                creatingObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;

            case PrimitiveType.Cube:
                creatingObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;

            case PrimitiveType.Cylinder:
                creatingObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                break;
        }

        creatingObject.transform.position = position;
    }

    // モデル保存機能
    private void SaveModel()
    {
        // ... (VRChatのイベントハンドラーを用いてモデルをローカルに保存する処理を実装)
    }

    // モデル読み込み機能
    private void LoadModel()
    {
        // ... (VRChatのイベントハンドラーを用いてローカルからモデルを読み込む処理を実装)
    }
}
