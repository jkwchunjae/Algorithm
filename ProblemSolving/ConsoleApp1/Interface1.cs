using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal interface Interface1
    {
    }

	// IMap이 추상화하는 것

	// ICell이 추상화하는 것
	
	// 플레이어가 interact 할 수 있는게,
	// 1. 벽; 2. 가시; 3. 몬스터; 4. 아이템 박스; 5. 빈칸
	// 5개가 구현하는 게 Cell.
	// Cell안에 GetInteractionResult. 최종 플레이어 위
	// Interaction 결과로 돌려줘야 하는 정보: interaction 후의 플레이어의 좌표, 

	// Interaction의 결과가 추상화해야 하는 것
	// 

	//기타
	// 필요한 건: 1. 들어갈 수 있는지 보는 것, 2. 들어가서 interaction이 일어나는 것
	// 맵에다가 어떤 좌표로 이동할 것이다라는 정보를 전달해준다.
	// 게임 엔진이 있다. 얘가 모든 걸 통제해줘야(브로커랄까)
	// 모든 명령은 게임 엔진한테 주고,
	// 예를 들어, 플레이어가 게임엔진한테 어디어디 가겠습니다 하면
	// 게임 엔진이 그 정보를 잘 가공해서 모든 이해당사자들(맵마저도 이해당사자)한테 어떤 데이터를 주고, 이해당사자들이 데이터를 받아서 자신의 상태를 변경한다.
	// 게임 엔진이 어떤 api를 가져야 할까?
	// interaction 처리인데, 아까 염려했던 부분이 interaction의 유형이 너무 다양하다. 5종류인데 한 번에 묶어서 생각하기에는 서로 관련이 크지 않아보인다.
	// interaction result 종류에 상관 없이 공통적으로 가져야 하는 것: 좌표값, 
	// 두 가지를 생각해야 하지 않을까? interaction의 결과로 셀이 어떤 대상물을 들고 있어야 하는지, 셀에 있던 대상물의 상태는 어떻게 변해야 하는지
	// 벽의 경우: 벽을 들고 있던 셀은 그대로 벽을 들고 있고, 벽도 아무런 영향이 없다 && 플레이어를 들고 있던 셀은 그대로 플레이어를 들고 있고, 플레이어도 아무 변화가 없다.
	// 가시함정의 경우: 가시함정을 들고 있던 셀은 그대로 가시함정을 들고 있고, 경우에 따라 플레이어도 추가로 들 수 있고, 가시함정은 아무런 영향이 없다 && 플레이어를 들고 있던 셀은 아무것도 들고 있지 않게 되고, 플레이어는 데미지를 입게 되거나 죽는다.
	//
	//
	// ** 데미지 주는 대상의 타입 정보 필요: 일반 몬스터인지, 보스 몬스터인지, 가시함정인지. 이 정보랑 데미지 수치 정보를 플레이어가 받아서 자기가 입어야 하는 피해량을 계산할 수 있다.(장신구는 플레이어가 들고 있으니)



	public interface ICell
	{
		Position Position {get; init;}

		IInteractable Interatable { get; }

		void Interact(IPlayer player);
	}

	public interface IItem
	{

	}

	public interface IInteractable
	{
		InteractResult Interact(IPlayer player);
	}

	public interface IBlank : IInteractable
	{

	}

	public interface IWall : IInteractable
	{

	}

	public interface IItemBox : IInteractable
	{

	}

	public interface IMonster : IInteractable
	{
	}

	public interface ITrap : IInteractable
	{
		
	}

	public class InteractResult
	{
		bool Dead { get; } // 장신구 없음
		bool ChangeToBlank { get; }
	}

	public interface IPlayer
	{
		Position Position { get; set; }

		int Experience { get; set; }
		int Level { get; set; }
		
		int MaxHP { get; set; }
		int CurrentHP { get; set; }

		int AttackValue { get; set; }
		int DefenseValue { get; set; }
		
		IWeapon Weapon { get; set; }
		IArmor Armor { get; set; }
		IOrnament[] Ornaments { get; set; }
	}
}
