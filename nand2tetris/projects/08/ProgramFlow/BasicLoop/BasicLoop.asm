//initialization
@256
D=A
@SP
M=D
@300
D=A
@LCL
M=D
@400
D=A
@ARG
M=D
@3000
D=A
@THIS
M=D
@3010
D=A
@THAT
M=D
@5
D=A
@TMP
M=D
//initialization end
//start Push constant 0
@0
D=A
@SP
A=M
M=D
@SP
M=M+1
//end Push constant 0
//start Pop local 0
//Local0
@LCL
D=M
@0
D=D+A
@R13
M=D
@SP
M=M-1
@SP
A=M
D=M
@R13
A=M
M=D
//endLocal0
//end Pop local 0
(BasicLoop.LOOP_START)
//start Push argument 0
@ARG
D=M
@0
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//end Push argument 0
//start Push local 0
@LCL
D=M
@0
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//end Push local 0
//add
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=D+M
@SP
A=M
M=D
@SP
M=M+1
//add end
//start Pop local 0
//Local0
@LCL
D=M
@0
D=D+A
@R13
M=D
@SP
M=M-1
@SP
A=M
D=M
@R13
A=M
M=D
//endLocal0
//end Pop local 0
//start Push argument 0
@ARG
D=M
@0
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//end Push argument 0
//start Push constant 1
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
//end Push constant 1
//sub
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=M-D
@SP
A=M
M=D
@SP
M=M+1
//sub end
//start Pop argument 0
//Argument0
@ARG
D=M
@0
D=D+A
@R13
M=D
@SP
M=M-1
@SP
A=M
D=M
@R13
A=M
M=D
//endArgument0
//end Pop argument 0
//start Push argument 0
@ARG
D=M
@0
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//end Push argument 0
@SP
M=M-1
@SP
A=M
D=M
@BasicLoop.LOOP_START
D;JNE
//start Push local 0
@LCL
D=M
@0
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//end Push local 0
