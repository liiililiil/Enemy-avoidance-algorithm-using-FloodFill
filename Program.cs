int[,] arr = new int[20,20];
bool isPass;
List<int> wayx = new List<int>();
List<int> wayy = new List<int>();

for(int i = 0; i < arr.GetLength(0)-1; i++){
        for(int j = 0; j < arr.GetLength(1)-1; j++){
            arr[i,j] = 0;
        }
}

int nowvalue = 0;


//여기부터 수정//


//적 데미지
int emydmg = 5;

//공격 사거리
int emyrange = 5;

//여기에 플레이어 좌표를 입력
int playerx = 9; int playery = 9;


// 이함수를 이용해 적을 추가
enemy(17,19);
enemy(1,1);
enemy(19,0);
enemy(15,0)
;
enemy(0,4);
enemy(0,15);
enemy(3,15);





//여기부터는 건들지 말것//
move();

Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("결과");

for(int i = 0; i < arr.GetLength(0); i++){
    for(int j = 0; j<arr.GetLength(1); j++){
        isPass = false;
        for(int k = 0; k < wayx.Count-1; k++){
            if(i == wayx[k] && j == wayy[k]) isPass = true;
        }

        if(isPass) Console.Write("|| ");
        else Write(arr[i,j]);


    }

    Console.WriteLine("");
}

Console.WriteLine("");

for(int i = 0; i < wayx.Count-1; i++){
    Console.Write("["+wayx[i]+","+wayy[i]+"]"+" ");
}




void move(){ //플레이어 위치 이동시켜 길찾기 진행
    player(playerx,playery);
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("다음 회차");
    print(0,0);
    Console.WriteLine("길찾기 결과");
    findway(playerx,playery);
}

void findway(int x, int y)
{
    if (x == 0 || x == arr.GetLength(0) - 1 || y == 0 || y == arr.GetLength(1) - 1)
    {
        wayx.Add(x);
        wayy.Add(y);
        return;
    }

    int i = x, j = y;
    int min = int.MaxValue; //최솟값 비교를 위한 큰 수로 초기화
    nowvalue = arr[x, y];

    //상하좌우에서 현재 값보다 크고, 최솟값보다 작은 값을 찾습니다.
    if (x > 0 && arr[x - 1, y] > nowvalue && arr[x - 1, y] < min)
    {
        min = arr[x - 1, y];
        i = x - 1;
        j = y;
    }
    if (x < arr.GetLength(0) - 1 && arr[x + 1, y] > nowvalue && arr[x + 1, y] < min)
    {
        min = arr[x + 1, y];
        i = x + 1;
        j = y;
    }
    if (y > 0 && arr[x, y - 1] > nowvalue && arr[x, y - 1] < min)
    {
        min = arr[x, y - 1];
        i = x;
        j = y - 1;
    }
    if (y < arr.GetLength(1) - 1 && arr[x, y + 1] > nowvalue && arr[x, y + 1] < min)
    {
        min = arr[x, y + 1];
        i = x;
        j = y + 1;
    }


    // 만약 이동이 없을 경우 (최솟값이 여전히 초기값이면) 함수를 종료합니다.
    if (min == int.MaxValue || min == nowvalue)
    {
        // 이미 방문한 경로인 경우 종료
        if (wayx.Contains(playerx) && wayy.Contains(playery)) return;
        arr[playerx, playery] = 0;
        unset(playerx, playery);

        // 위치를 초기화하고 다시 이동 시도
        playerx = x;
        playery = y;
        if (wayx.Count > 0) wayx.RemoveAt(wayx.Count - 1);
        if (wayy.Count > 0) wayy.RemoveAt(wayy.Count - 1);
        move();
        return;
    }

    // 현재 위치를 경로로 추가하고 다음 위치로 이동
    wayx.Add(i);
    wayy.Add(j);
    findway(i, j);
}

void player(int i,int j){
    playerx= i; playery = j;
    set(i,j);
}

void enemy(int i, int j){
    arr[i,j] = -1;
    set(i,j);
    add(i,j);
}

void set(int x,int y){ // floodfill
    for(int i = 0; i < arr.GetLength(0); i++){
        for(int j = 0; j < arr.GetLength(1); j++){
            if(arr[i,j] <=-1) continue;
            arr[i,j] += Math.Max(Math.Abs(i - y),Math.Abs(j - x));
        }
    }
}

void add(int x,int y){  //x ,y에 emyrange공간을 emydmg만큼 추가함.
    for(int i = Math.Max(0,x-(emyrange-1)/2); i <= Math.Min(19,x+(emyrange-1)/2); i++){
        for(int j = Math.Max(0,y-(emyrange-1)/2); j <= Math.Min(19,y+(emyrange-1)/2); j++){
            if(arr[i,j] <=-1) {

            } else{
                arr[i,j] = Math.Max(arr[i,j] -emydmg,0);
            }
            
        }
    }
}

void unset(int x,int y){ //floodflow를 해체함.
    for(int i = 0; i < arr.GetLength(0); i++){
        for(int j = 0; j < arr.GetLength(1); j++){
            if(arr[i,j] <=0) continue;
            arr[i,j] = Math.Max(arr[i,j] -Math.Max(Math.Abs(i - y),Math.Abs(j - x)),0);
        }
    }
}

void print(int i,int j){ //배열출력
    if(i >= arr.GetLength(0)) return;
    if(j >= arr.GetLength(1)){
        Console.WriteLine();
        print(i+1,0);
        return;
    }

    Write(arr[i,j]);
    print(i,j+1);
}

void Write(int i){ //배열크기 맞추기
    if(i >= 10 || i <= -1) Console.Write(i + " ");
    else Console.Write(i + "  ");
}