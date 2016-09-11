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
//start Push argument 1
@ARG
D=M
@1
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//end Push argument 1
//start Pop pointer 1
//Pointer1
@THAT
D=A
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
//endPointer1
//end Pop pointer 1
//start Push constant 0
@0
D=A
@SP
A=M
M=D
@SP
M=M+1
//end Push constant 0
//start Pop that 0
//That0
@THAT
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
//endThat0
//end Pop that 0
//start Push constant 1
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
//end Push constant 1
//start Pop that 1
//That1
@THAT
D=M
@1
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
//endThat1
//end Pop that 1
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
//start Push constant 2
@2
D=A
@SP
A=M
M=D
@SP
M=M+1
//end Push constant 2
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
(FibonacciSeries.MAIN_LOOP_START)
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
@FibonacciSeries.COMPUTE_ELEMENT
D;JNE
@FibonacciSeries.END_PROGRAM
0;JMP
(FibonacciSeries.COMPUTE_ELEMENT)
//start Push that 0
@THAT
D=M
@0
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//end Push that 0
//start Push that 1
@THAT
D=M
@1
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//end Push that 1
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
//start Pop that 2
//That2
@THAT
D=M
@2
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
//endThat2
//end Pop that 2
//start Push pointer 1
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1
//end Push pointer 1
//start Push constant 1
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
//end Push constant 1
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
//start Pop pointer 1
//Pointer1
@THAT
D=A
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
//endPointer1
//end Pop pointer 1
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
@FibonacciSeries.MAIN_LOOP_START
0;JMP
(FibonacciSeries.END_PROGRAM)
