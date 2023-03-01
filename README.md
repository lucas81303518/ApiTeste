ApiTeste
Caso queira utilizar outro arquivo .csv na API é só alterar o arquivo do projeto no caminho ApiTeste/ApiTeste/ArquivoCsv/movielist.csv e manter esse mesmo nome no arquivo.
Verbo: (GET)
EndPoint: https://localhost:7196/PioresFilmes
Ele retornará um Json com maior intervalo entre dois prêmios consecutivos, e o que obteve dois prêmios mais rápido.
Modelo do Json:
{
  "min": [
    {
      "producer": "Joel Silver",
      "interval": 1,
      "previousWin": 1990,
      "followingWin": 1991
    }
  ],
  "max": [
    {
      "producer": "MATTHEW VAUGHN",
      "interval": 13,
      "previousWin": 2002,
      "followingWin": 2015
    }
  ]
}
