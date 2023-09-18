using UnityEngine;
using LitJson;
using System.IO;
using System.Threading.Tasks;

// �ڳ� SDK namespace �߰�
using BackEnd;

public class BackendManager : MonoBehaviour
{
	TextAsset textAsset;
	void Start()
	{
		var bro = Backend.Initialize(true); // �ڳ� �ʱ�ȭ

		// �ڳ� �ʱ�ȭ�� ���� ���䰪
		if (bro.IsSuccess())
		{
			Debug.Log("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 204 Success
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Debug.LogError("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 400�� ���� �߻� 
		}

		Test();
	}

	void DebugHello()
	{
		Debug.Log("Hello");
	}

	// ���� �Լ��� �񵿱⿡�� ȣ���ϰ� ���ִ� �Լ�(����Ƽ UI ���� �Ұ�)
	async void Test()
	{
		await Task.Run(() => {
			/*BackendLogin.Instance.CustomLogin("user1", "1234"); // �ڳ� �α��� �Լ�

			BackendRank.Instance.RankInsert(1000); // [�߰�] ��ŷ �ҷ����� �Լ�

			Debug.Log("�׽�Ʈ�� �����մϴ�.");*/
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