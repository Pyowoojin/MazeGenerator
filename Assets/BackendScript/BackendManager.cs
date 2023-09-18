using UnityEngine;
using LitJson;
using System.IO;
using System.Threading.Tasks;

// 뒤끝 SDK namespace 추가
using BackEnd;

public class BackendManager : MonoBehaviour
{
	TextAsset textAsset;
	void Start()
	{
		var bro = Backend.Initialize(true); // 뒤끝 초기화

		// 뒤끝 초기화에 대한 응답값
		if (bro.IsSuccess())
		{
			Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생 
		}

		Test();
	}

	void DebugHello()
	{
		Debug.Log("Hello");
	}

	// 동기 함수를 비동기에서 호출하게 해주는 함수(유니티 UI 접근 불가)
	async void Test()
	{
		await Task.Run(() => {
			/*BackendLogin.Instance.CustomLogin("user1", "1234"); // 뒤끝 로그인 함수

			BackendRank.Instance.RankInsert(1000); // [추가] 랭킹 불러오기 함수

			Debug.Log("테스트를 종료합니다.");*/
			BackendLogin.Instance.CustomLogin("user2", "1234");

			string newIndate = "2023-01-24T15:44:17.196Z";

			string inDate = Backend.UserInDate;

			var bro = Backend.GameData.GetMyData("Tetris_score", newIndate);

			var json = bro.GetReturnValuetoJSON();

			LitJson.JsonData gameData = bro.FlattenRows();

			int myscore = int.Parse(gameData[0]["Score"].ToString());

			/*Debug.Log(myscore);*/

			

			Debug.Log(json);
			

			// Debug.Log(bro + "indate : " + newIndate);

		});
	}

	

}