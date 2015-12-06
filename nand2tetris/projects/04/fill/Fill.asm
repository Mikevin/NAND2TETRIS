// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Fill.asm

// Runs an infinite loop that listens to the keyboard input.
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel. When no key is pressed, the
// program clears the screen, i.e. writes "white" in every pixel.
// screen is 16384 - 24576 (8K)
(LOOP)
      @R0
      M=0
      @SCREEN
      D=A
      @R1
      M=D

      @KBD
      D=M
      @SETBLACK
      D;JNE
      @FILL
      0;JMP
      @LOOP
      D;JEQ
(SETBLACK)
      @0
      D=A-1
      @R0
      M=D
      @FILL
      0;JMP
(FILL)
      @R1
      D=M
      @24576
      D=D-A
      @LOOP
      D;JEQ //check if whole screen filled

      @R0
      D=M
      @R1
      A=M
      M=D
      //up the pixel counter
      @R1
      M=M+1
      @FILL
      0;JMP
