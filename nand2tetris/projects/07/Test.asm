//put 20 in @this slot
@20
D=A
@THIS
M=D


//put 5 in slot 26
@5
D=A
@26
M=D

//increase SP
@SP
M=M+1

//grab location of THIS stack
@THIS
D=M
//index 6th entry of THIS stack
@6
A=A+D
D=M
//write value to current SP
@SP
A=M
M=D
//inc SP
@SP
M=M+1

//decr SP
@SP
M=M-1
//grab SP val
A=M
D=M
//write val to @VAL
@VAL
M=D

//grab location of THIS stack
@THIS
D=M
//index 6th entry of THIS stack
@6
D=A+D

//store address in @ADDR
@ADDR
M=D
//grab value from VAL
@VAL
D=M
//store val in saves ADDR
@ADDR
M=D
