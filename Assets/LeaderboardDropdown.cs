using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardDropdown : MonoBehaviour
{
    public TMP_Dropdown levelDropdown;

    public PlayfabManager playfabManager;

    private void Start()
    {
        // Adicione um listener para a mudança de valor no dropdown
        levelDropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        // Inicialmente, chame a função com o valor padrão
        playfabManager.GetLeaderboard(1); // Supondo que o nível padrão seja 1
    }

    private void OnDropdownValueChanged(int value)
    {
        // O valor 'value' corresponde ao índice selecionado no dropdown
        int selectedLevel = value + 1; // Adicionando 1 para corresponder ao seu exemplo

        // Chame a função GetLeaderboard com o nível selecionado
        playfabManager.GetLeaderboard(selectedLevel);
    }

}
