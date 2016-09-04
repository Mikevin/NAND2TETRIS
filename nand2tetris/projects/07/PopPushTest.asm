//stores 5 on stack, then pushes it onto THIS[6](addr 2048+6)

//initialization
  //set SP to 256
  @256
  D=A
  @SP
  M=D

  //set THIS to 2048
  @2048
  D=A
  @THIS
  M=D

//pop constant 5
  @5
  D=A
  @SP
  A=M
  M=D

  //inc SP
  @SP
  M=M+1

//push THIS 6
//decr SP
@SP
M=M-1
//get THIS index 6 ADDR
@THIS
D=M
//store in R1
@R1
M=D
//add 6 to location in R1
@6
D=A
@R1
M=M+D
//get value from SP
@SP
A=M
D=M
//store in address found in R1
@R1
A=M
M=D
