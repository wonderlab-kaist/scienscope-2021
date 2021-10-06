# scienscope-2021
기기들 블루투스 주소값
![image](https://user-images.githubusercontent.com/10079436/134457982-ee052289-b159-40cd-8549-b3d7277694dc.png)
왼쪽 부터 순서대로
1. F8:D8:39:47:6D:E4 (붉은색 링, 안쪽에 검은색 마킹)
2. EC:47:D7:D6:E7:6C (붉은색 링, 안쪽에 마킹 없음)
3. ED:41:B5:30:7F:BE (노란색 링)
5. C5:31:BA:E3:E2:6A (민트색 링)

21-08-26 
 - camera_movement.cs에서 scene load시에 rotation 위치가 일시적으로 안맞는 부분 해결
 - camera_movement.cs의 cam rotation관련 수정
 - RFID 인식이 잘 안되던 부분, dataInput.cs 수정
 - aarcall.cs update에서 data 불러오는 대신 20ms 단위로 균일하게 호출하게 변경

21-09-18
 - dataInput.cs에서 rfid 인식시 D를 기준으로 split하는 바람에 생기는 에러 수정
