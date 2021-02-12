# ImageGalleryListView

에뮬레이터 통합 프론트엔드 "슈퍼소니코 게임센터"

여러 버전의 마메, 파이널번과 같은 아케이드 게임.
버츄어 NES, SNES, MD Fusion, VisualBoyAdvance, Epsxe, Mednafe와 같은 콘솔 게임.
위 에뮬레이터를 게임과 연결 해주는 역할을 합니다.
에뮬레이터의 어떠한 설정도 건드리지 않습니다.

잘 실행이 되는 지 확인을 마치고, XML과 스냅샷을 잘 정리해 두었다면 
다음번에 다시 켤때 빠르게 켤 수 있도록 간소화된 프론트엔드 입니다.



● 개발 철학
1. 음악이 좋은
게임음악을 꿈속에서 들을 정도로 좋아합니다. 
가끔 정말 듣고 싶을때 게임을 켜서 듣기도 합니다.
프론트엔드에서 내는 사운드 잡음, BGM등은 객기라 생각 합니다.

2. 추억소환에 방해 받지 않게
오프닝 스플래시, 로고, UI등을 최대한 자제하여 구현 하였습니다.
슈퍼소니코 게임센터는 모자쓴 초딩 로고도 번쩍 거리는 화면도 없습니다.

3. 스틱과 패드 그때의 손과 촉감
스틱으로 게임 할때, 패드로 게임 할때 그때마다 다른 에뮬레이터를 사용합니다.
예를들면 슈퍼패미컴의 파이널판타지6는 패드와 어울리는 게임이고,
고스트 스위퍼 미카미 제령사는 나이스바디는 스틱과 어울리는 게임입니다.
또한 킹오파는 4버튼을 사용하며, 스파는 6버튼을 사용합니다.
그때그때 설정을 바꿀 필요 없이 한번 설정 해두면 그 에뮬레이터로 연결 하도록 관리합니다.

4. 꺼진 불 검은 조명 홀로 빛나는 TV화면
4:3 모니터를 사용하고 있어 화면 비율에 대해 신경쓰진 않았습니다.
에뮬레이터의 설정에 기반합니다. 

5. 잠깐 켜는 것도 하고 싶은(하는) 것이다.
오프닝만 보고 끄는 게임도 있고, 열심히 플레이 하는 게임도 있습니다. 
켜는게 빨라야 합니다. 또한 끄고 다른것을 켜는것도 빨라야 합니다.




● 바이너리 파일은 구글 드라이브내 공유
https://drive.google.com/drive/folders/1xmbfaLTZZE0gm2kclc1iCkQtRNLO4xid?usp=sharing
닷넷프레임워크 4.5 기반으로 제작 되었습니다. (윈도우10이면 돌아감)




● 디렉터리 스트럭쳐 (롬공유는 불법임)
제가 정리한 롬과 에뮬레이터를 관리하는 폴더 구조 입니다.
E:\_Emul_ 디렉터리
[.]                              [..]                             [__GBA_ROM]
[__GBA_VisualBoyAdvance-PAD]     [__GBA_VisualBoyAdvance-STICK]   [__MAME0_ROM_0]
[__MAME1_mame32m]                [__MAME1_ROM_1]                  [__MAME2_106]
[__MAME2_ROM_2]                  [__MAME3_mame32m]                [__MAME3_ROM_1]
[__MAME4_106]                    [__MAME4_ROM_2]                  [__MAME5_mame32m]
[__MAME5_ROM_1]                  [__MAME6_106]                    [__MAME6_ROM_2]
[__MAME7_mame32m]                [__MAME7_ROM_1]                  [__MAME8_106]
[__MAME8_ROM_2]                  [__MAME9_106]                    [__MAME9_ROM_2]
[__MAME_SPRT_FBA]                [__MAME_SPRT_FBA2]               [__MAME_SPRT_ROM_0]
[__MD_gens-win32-bin-2.14]       [__MD_Megadrive_Fusion]          [__MD_ROM]
[__NDS_desmume-0.9.9-win64]      [__NDS_ROM]                      [__NES_ROM]
[__NES_virtuanes097e-1722-PAD]   [__NES_virtuanes097e-1722-STICK] [__PS1_ePSXe195]
[__PS1_ROM]                      [__SFC_ROMS]                     [__SFC_snes9x-Pad]
[__SFC_snes9x-Stick]             [__SS_mednafen-PAD]              [__SS_mednafen-STICK]
[__SS_ROMS]




● config.ini수정
[setup]
DriveName=E:
// **1. 에뮬레이터가 깔려있는 드라이브 명
EmulatorRoot=_Emul_
// **2. 에뮬레이터와 롬들이 깔려 있는 루트 폴더 이름
SnapShotFolderName=__super_sonico_snap
AssetsFolderName=__super_sonico_assets
ArcadeXml=arcade.xml
ConsoleXml=console.xml
ShortCutSheet=shortcut.png
// 위에것은 건드릴 필요는 없습니다. 
 **1 , **2를 수정하면 됩니다.



● 게임을 프론트엔드에 등록 하는법. (XML 정리 지옥!!!)
1. 게임이 잘되는지 에뮬레이터를 이용하여 구동 시킵니다.
2. 스냅샷(게임이 진행되는 장면이나, 해당 게임을 떠올리면 머릿속에 남는 사진 한장)을 찍습니다. 
3. 스냅샷을 __super_sonico_snap 폴더에 집어 넣습니다.
4. __super_sonico_assets 폴더의 arcade.xml 혹은 console.xml을 엽니다. 
5. XML구조
5.1 아케이드의 경우
    <?xml version='1.0' encoding="UTF-8" ?>
    <arcade>
      <games genre="벨트스크롤">
        <game>
          <name>던전드래곤 타워 오브 둠</name>
          <snap>ddtod.png</snap>
          <file>ddtodar1.zip</file>
          <emul>__MAME1_mame32m</emul>
          <roms>__MAME1_ROM_1</roms>
          <exec>_MAME1_mame32m_.exe</exec>
          <option></option>
        </game>
      </games>
      <games genre="대전액션">
      </games>
      .
      .
      .
    </arcade>
 여기서 <game>엘레멘트를 추가하며 게임을 프론트엔드에 등록 합니다.
 각 xml 엘리먼트들은 아래와 같은 의미를 가지고 있습니다.
   name : 게임이 프론트엔드에 출력되는 이름 입니다. 
   snap : 스냅샷의 파일명 입니다.
   file : 실제 실행되는 게임 롬파일명 입니다.
   emul : 해당 게임을 실행하는 에뮬레이터가 들어있는 폴더명 입니다.
   roms : 해당 게임이 들어있는 폴더명 입니다.
   exec : 마메/파이널번 실행 파일명입니다.
   option : 파이널번에만 해당되는 경우로 -w를 넣으면 윈도우 화면으로 출력됩니다.
 
 아케이드는 games라는 엘리먼트에 genre라는 속성을 가지고 있으나 생략해도 무관합니다.
 맞습니다. 이 프론트 엔드는 XML정리 지옥인 것입니다.!!!
 
 
5.2 콘솔의 경우 
  <?xml version="1.0" encoding="UTF-8"?>
  <console>
      <games machine="NES">
          <game>
              <name>스틱)개구쟁이 요리사군의 구루메월드 (E)</name>
              <snap>개구쟁이 요리사군의 구루메월드 (E).png</snap>
              <file>개구쟁이 요리사군의 구루메월드 (E).nes</file>
              <emul>__NES_virtuanes097e-1722-STICK</emul>
              <roms>__NES_ROM</roms>
              <exec>_NES_VirtuaNES.exe</exec>
          </game>
     </games>
     .
     .
     .
     <games machine="SS">
          <game>
              <name>패드)슈퍼로봇대전 F</name>
              <snap>SRW F(JP) - [V2.002].png</snap>
              <file>SRW F(JP) - [V2.002].ccd</file>
              <emul>__SS_mednafen-PAD</emul>
              <roms>__SS_ROMS</roms>
              <exec>mednafen.exe</exec>
          </game>
          <game>
              <name>패드)슈퍼로봇대전 F - 완결편</name>
              <snap>SRW F C(JP) - [V2.000][10].png</snap>
              <file>SRW F C(JP) - [V2.000][10].ccd</file>
              <emul>__SS_mednafen-PAD</emul>
              <roms>__SS_ROMS</roms>
              <exec>mednafen.exe</exec>
          </game>
          <game>
              <name>스틱)아웃런</name>
              <snap>OUT RUN (JP).png</snap>
              <file>OUT RUN (JP).ccd</file>
              <emul>__SS_mednafen-PAD</emul>
              <roms>__SS_ROMS</roms>
              <exec>mednafen.exe</exec>
          </game>
    </games>
  </console>  
   여기서 <game>엘레멘트를 추가하며 게임을 프론트엔드에 등록 합니다.
 콘솔은 games라는 엘리먼트에 machine이라는 속성을 가지고 있습니다. "NES", "SNES", "MD", "GBA", "SS" 혹은 "PS1"이라는 값을 가지며
 PS1의 경우 memc라는 메모리카드 경로를 지정해주어야 합니다.
         <game>
            <name>패드)악마성-월하의야상곡(k)</name>
            <snap>악마성-월하의야상곡(k).png</snap>
            <file>악마성-월하의야상곡(k).bin</file>
            <memc>악마성-월하의야상곡(k).mcr</memc>
            <emul>__PS1_ePSXe195</emul>
            <roms>__PS1_ROM</roms>
            <exec>_PS1_ePSXe195.exe</exec>
        </game>
        
제가 사용하던 XML은 그대로 남겨 두었습니다.

 
=ㅅ=;
