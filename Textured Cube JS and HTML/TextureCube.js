// Anthony Jordan
"use strict";

var canvas;
var gl;
var program;
var happyFaceImage;

var numVertices = 36;

var texSize = 64;
var imgSize = 64;
var numChecks = 4;

var checkerImage = new Uint8Array(4 * imgSize * imgSize);
for (var i = 0; i < imgSize; i++) {
    for (var j = 0; j < imgSize; j++) {
        var patchx = Math.floor(i / (imgSize / numChecks));
        var patchy = Math.floor(j / (imgSize / numChecks));
        var c;
        if (patchx % 2 ^ patchy % 2)
            c = 255;
        else
            c = 0;
        checkerImage[4 * i * imgSize + 4 * j] = c;
        checkerImage[4 * i * imgSize + 4 * j + 1] = c;
        checkerImage[4 * i * imgSize + 4 * j + 2] = c;
        checkerImage[4 * i * imgSize + 4 * j + 3] = 255;
    }
}


var pointsArray = [];
var colorsArray = [];

var texCoordsArray0 = [];
var texCoord0 = [
    vec2(0, 0),
    vec2(0, 1),
    vec2(1, 1),
    vec2(1, 0)
];

var texCoordsArray1 = [];
var texCoord1 = [
    vec2(0, 0),
    vec2(0, 4),
    vec2(4, 4),
    vec2(4, 0)
];

var texture0, texture1;



var vertices = [
    vec4(-0.5, -0.5, 0.5, 1.0),
    vec4(-0.5, 0.5, 0.5, 1.0),
    vec4(0.5, 0.5, 0.5, 1.0),
    vec4(0.5, -0.5, 0.5, 1.0),
    vec4(-0.5, -0.5, -0.5, 1.0),
    vec4(-0.5, 0.5, -0.5, 1.0),
    vec4(0.5, 0.5, -0.5, 1.0),
    vec4(0.5, -0.5, -0.5, 1.0)
];


var vertexColors = [
    vec4(0.0, 0.0, 0.0, 1.0),  // black
    vec4(1.0, 0.0, 0.0, 1.0),  // red
    vec4(1.0, 1.0, 0.0, 1.0),  // yellow
    vec4(0.0, 1.0, 0.0, 1.0),  // green
    vec4(0.0, 0.0, 1.0, 1.0),  // blue
    vec4(1.0, 0.0, 1.0, 1.0),  // magenta
    vec4(0.0, 1.0, 1.0, 1.0),  // white
    vec4(0.0, 1.0, 1.0, 1.0)   // cyan
];
window.onload = init;


var xAxis = 0;
var yAxis = 1;
var zAxis = 2;
var axis = xAxis;

var theta = [45.0, 45.0, 45.0];

var thetaLoc;


function init() {
    canvas = document.getElementById("gl-canvas");

    gl = WebGLUtils.setupWebGL(canvas);
    if (!gl) { alert("WebGL isn't available"); }

    gl.viewport(0, 0, canvas.width, canvas.height);
    gl.clearColor(1.0, 1.0, 1.0, 1.0);

    gl.enable(gl.DEPTH_TEST);

    //
    //  Load shaders and initialize attribute buffers
    //
    program = initShaders(gl, "vertex-shader", "fragment-shader");
    gl.useProgram(program);

    colorCube();

    var cBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, cBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, flatten(colorsArray), gl.STATIC_DRAW);
    var vColor = gl.getAttribLocation(program, "vColor");
    gl.vertexAttribPointer(vColor, 4, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(vColor);

    var vBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, vBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, flatten(pointsArray), gl.STATIC_DRAW);
    var vPosition = gl.getAttribLocation(program, "vPosition");
    gl.vertexAttribPointer(vPosition, 4, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(vPosition);


    var tBuffer0 = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, tBuffer0);
    gl.bufferData(gl.ARRAY_BUFFER, flatten(texCoordsArray0), gl.STATIC_DRAW);
    var vTexCoord0 = gl.getAttribLocation(program, "vTexCoord0");
    gl.vertexAttribPointer(vTexCoord0, 2, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(vTexCoord0);

    var tBuffer1 = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, tBuffer1);
    gl.bufferData(gl.ARRAY_BUFFER, flatten(texCoordsArray1), gl.STATIC_DRAW);
    var vTexCoord1 = gl.getAttribLocation(program, "vTexCoord1");
    gl.vertexAttribPointer(vTexCoord1, 2, gl.FLOAT, false, 0, 0);
    gl.enableVertexAttribArray(vTexCoord1);


    happyFaceImage = document.getElementById("smileyimage"); 



    configureTexture0(checkerImage);

    configureTexture1(happyFaceImage);

    gl.activeTexture(gl.TEXTURE0);
    gl.bindTexture(gl.TEXTURE_2D, texture0);
    gl.uniform1i(gl.getUniformLocation(program, "texture0"), 0);

    gl.activeTexture(gl.TEXTURE1);
    gl.bindTexture(gl.TEXTURE_2D, texture1);
    gl.uniform1i(gl.getUniformLocation(program, "texture1"), 1);



    thetaLoc = gl.getUniformLocation(program, "theta");

    document.getElementById("ButtonX").onclick = function () { axis = xAxis; };
    document.getElementById("ButtonY").onclick = function () { axis = yAxis; };
    document.getElementById("ButtonZ").onclick = function () { axis = zAxis; };

    render();
}

function configureTexture0(image) {
    texture0 = gl.createTexture();
    gl.bindTexture(gl.TEXTURE_2D, texture0);
    gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, true);
    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, texSize, texSize, 0,
        gl.RGBA, gl.UNSIGNED_BYTE, image);
    gl.generateMipmap(gl.TEXTURE_2D);
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER,
        gl.NEAREST_MIPMAP_LINEAR);
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.NEAREST);
}

function configureTexture1(image) {
    texture1 = gl.createTexture();
    gl.bindTexture(gl.TEXTURE_2D, texture1);
    gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, true);
    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGB,
        gl.RGB, gl.UNSIGNED_BYTE, image);
    gl.generateMipmap(gl.TEXTURE_2D);
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER,
        gl.NEAREST_MIPMAP_LINEAR);
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.NEAREST);
}


function quad(a, b, c, d) {

    pointsArray.push(vertices[a]);
    colorsArray.push(vertexColors[a]);

    texCoordsArray0.push(texCoord0[0]);
    texCoordsArray1.push(texCoord1[0]);

    pointsArray.push(vertices[b]);
    colorsArray.push(vertexColors[a]);

    texCoordsArray0.push(texCoord0[1]);
    texCoordsArray1.push(texCoord1[1]);

    pointsArray.push(vertices[c]);
    colorsArray.push(vertexColors[a]);

    texCoordsArray0.push(texCoord0[2]);
    texCoordsArray1.push(texCoord1[2]);

    pointsArray.push(vertices[a]);
    colorsArray.push(vertexColors[a]);

    texCoordsArray0.push(texCoord0[0]);
    texCoordsArray1.push(texCoord1[0]);

    pointsArray.push(vertices[c]);
    colorsArray.push(vertexColors[a]);

    texCoordsArray0.push(texCoord0[2]);
    texCoordsArray1.push(texCoord1[2]);

    pointsArray.push(vertices[d]);
    colorsArray.push(vertexColors[a]);

    texCoordsArray0.push(texCoord0[3]);
    texCoordsArray1.push(texCoord1[3]);
}

function colorCube() {
    quad(1, 0, 3, 2);
    quad(2, 3, 7, 6);
    quad(3, 0, 4, 7);
    quad(6, 5, 1, 2);
    quad(4, 5, 6, 7);
    quad(5, 4, 0, 1);
}

var render = function () {
    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
    theta[axis] += 2.0;
    gl.uniform3fv(thetaLoc, theta);
    gl.drawArrays(gl.TRIANGLES, 0, numVertices);
    requestAnimFrame(render);
}