int[,] arr = new int[20,20];
List<int> wayx = new List<int>();
List<int> wayy = new List<int>();
for(int i = 0; i < arr.GetLength(0)-1; i++){
        for(int j = 0; j < arr.GetLength(1)-1; j++){
            arr[i,j] = 0;
        }
}

//적 데미지
int emydmg = 5;

//공격 사거리
int emyrange = 5;

//여기에 플레이어 좌표를 입력
int playerx = 9; int playery = 9;

// 이함수를 이용해 적을 추가
enemy(17,19);
//enemy(15,5);
enemy(9,17);
enemy(9,4);
enemy(3,7);
enemy(19,19);
enemy(0,0);
enemy(19,0);
enemy(0,19);


//여기부터는 건들지 말것
move();
Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("결과");
for(int i = 0; i < wayx.Count; i++){
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

void findway(int x,int y){ //길찾기
    
    int max =0;
    int i = x; int j = y;
    
    max =arr[x,y];


    if (x > 0 && max < arr[x-1, y]) {
        max = arr[x-1, y];
        i = x - 1;
    }
    if (x < arr.GetLength(0) - 1 && max < arr[x+1, y]) {
        max = arr[x+1, y];
        i = x + 1;
    }
    if (y > 0 && max < arr[x, y-1]) {
        max = arr[x, y-1];
        j = y - 1;
    }
    if (y < arr.GetLength(1) - 1 && max < arr[x, y+1]) {
        max = arr[x, y+1];
        j = y + 1;
    }

    //위 아래 양 옆에서 높은 쪽으로 이동

    if(max == arr[x,y]){ // 이동을 하지 않았으면
        if(wayx.Contains(playerx)&&wayy.Contains(playery)) return; //같은곳을 돌았으면 끝.
        arr[playerx,playery] = 0;
        unset(playerx,playery);
        playerx = x; playery = y;

        wayx.RemoveAt(wayx.Count-1);
        wayy.RemoveAt(wayy.Count-1);
        move(); //이동을 한후 다시 이동
        return;
    }
    else Console.Write(max + " ");

    
    wayx.Add(i);
    wayy.Add(j);
    findway(i,j);
}

void player(int i,int j){
    playerx= i; playery = j;
    arr[i,j] = -2;
    set(i,j);
}

void enemy(int i, int j){
    arr[i,j] = -1;
    set(i,j);
    add(i,j);
}

void set(int x,int y){ // floodflow 
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
    if(i >= arr.GetLength(0)-1) return;
    if(j >= arr.GetLength(1)-1){
        Write(arr[i,j]);
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