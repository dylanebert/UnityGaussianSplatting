void SelectAxes(in float3 X, in float3 Y, in float3 Z, out float3 x, out float3 y)
{
    float3 viewX = TransformWorldToViewDir(X);
    float3 viewY = TransformWorldToViewDir(Y);
    float3 viewZ = TransformWorldToViewDir(Z);

	float areaXY = abs(viewX.x * viewY.y - viewX.y * viewY.x);
	float areaYZ = abs(viewY.x * viewZ.y - viewY.y * viewZ.x);
	float areaXZ = abs(viewX.x * viewZ.y - viewX.y * viewZ.x);	

	float xyMask = step(areaYZ, areaXY) * step(areaXZ, areaXY);
	float yzMask = step(areaXY, areaYZ) * step(areaXZ, areaYZ);
    float xzMask = step(areaXY, areaXZ) * step(areaYZ, areaXZ);

	x = xyMask * X + yzMask * Y + xzMask * X;
	y = xyMask * Y + yzMask * Z + xzMask * Z;
}
