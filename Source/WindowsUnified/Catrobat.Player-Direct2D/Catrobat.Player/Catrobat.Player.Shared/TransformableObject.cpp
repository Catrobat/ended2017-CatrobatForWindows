#include "pch.h"
#include "TransformableObject.h"

using namespace D2D1;

TransformableObject::TransformableObject() :
m_objectScale(SizeF()),
m_opacity(0.f),
m_logicalSize(SizeF()),
m_ratio(SizeF()),
m_renderTargetSize(SizeF()),
m_rotation(0.f),
m_transformation(Matrix3x2F::Identity()),
m_translation(Point2F())
{
}

#pragma region SETTERS
#pragma endregion

#pragma region HELPER METHODS
void TransformableObject::RecalculateTransformation()
{
    if (m_look == NULL)
    {
        return;
    }
    m_renderTargetSize = m_look->GetBitMap()->GetSize();
    m_renderTargetSize.width *= m_ratio.width;
    m_renderTargetSize.height *= m_ratio.height;
    Matrix3x2F renderTarget = Matrix3x2F::Identity();
    renderTarget = Matrix3x2F::Translation(m_logicalSize.width / 2 - m_renderTargetSize.width / 2,
        m_logicalSize.height / 2 - m_renderTargetSize.height / 2);

    Matrix3x2F translation = Matrix3x2F::Translation(m_translation.x * m_ratio.width, m_translation.y * m_ratio.height) * renderTarget;
    D2D1_POINT_2F origin;
    origin.x = translation._31 + m_renderTargetSize.width / 2;
    origin.y = translation._32 + m_renderTargetSize.height / 2;

    m_transformation = translation *
        Matrix3x2F::Rotation(m_rotation, origin) *
        Matrix3x2F::Scale(m_objectScale, origin);
}
#pragma endregion
