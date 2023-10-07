<?php

namespace Database\Seeders;

use App\Models\StudyDocument;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class StudyDocumentSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        $i = 1;
        StudyDocument::create([
            'classroom_id' => "1",
            'content' =>
                "Sinh vật trong tự nhiên rất đa dạng, phong phú, chúng có thể là vi khuẩn, nấm, thực vật hay động vật...
            Các sinh vật sống ở nhiều môi trường khác nhau và có quan hệ mật thiết với nhau.
            Nghiên cứu các sinh vật nói chung và thực vật nói riêng để sử dụng chúng hợp lý là nhiệm vụ của Sinh học và Thực vật học.",
            'page' => $i,
        ]);

        $i++;
        StudyDocument::create([
            'classroom_id' => "1",
            'content' =>
                "Đặc điểm chung của thực vật:
                 - Tự tổng hợp chất hữu cơ.
                 - Phần lớn không có khả năng di chuyển.
                 - Phản ứng chậm với các kích thích từ bên ngoài.

                Thực vật có hoa thường có có quan sinh sản là hoa, quả, hạt.
                Thực vật không có hoa thì không sinh sản bằng hoa, quả hoặc hạt.",
            'page' => $i,
        ]);
        
        $i++;
        StudyDocument::create([
            'classroom_id' => "1",
            'content' =>
                "
                Rể cây
                Rễ cây có 2 loại rễ chính: rễ cọc và rễ chùm.
                 - Rễ cọc bào gồm rễ cái và các rễ con.
                 - Rễ chùm bao gồm các rễ con mọc từ gốc thân
                Rễ có 4 miền:
                  - Miền trưởng thành có chức năng dẫn truyền.
                  - Miền hấp thụ hấp thụ nước và muối khoáng.
                  - Miền sinh trưởng làm cho rễ dài ra.
                  - miền chóp rễ che cho đầu rễ.",
                'page' => $i,
        ]);

        $i++;
        StudyDocument::create([
            'classroom_id' => "1",
            'content' =>
                "Biến dạng của rễ cây:
                Một số loại rễ biến dạng để thực hiện các chức năng khác nhau như:
                 - Rễ củ chứa chất dinh dưỡng cho cây khi ra hoa, quả.
                 - Rễ móc bám vào các vật cản giúp cây leo lên.
                 - Rể thở giúp cây hô hấp trong không khí
                 ... 
                ",
                'page' => $i,
        ]);

        $i++;
        StudyDocument::create([
            'classroom_id' => "1",
            'content' =>
                "Thân cây
                Thân cây bao gốm: thân chính, cành, chồi ngọn và chồi nách.
                Tùy theo cách mọc của thân, người ta chia thân ra làm 3 loại: thân đứng, thân leo, thân bò.
                ",
                'page' => $i,
        ]);

        $i++;
        StudyDocument::create([
            'classroom_id' => "1",
            'content' =>
                "Thân cây dài ra do sự phân chia tế bào ở mô phân sinh ngọn.
                Để tăng nâng suất cây trồng, tùy từng loại cây, người ta sẽ bấm ngọn hoặc tỉa cành vào những thời diểm thích hợp
                
                Thân cây to ra do sự phân chia tế bào ở tầng sinh vỏ và tầng sinh trụ.
                Hằng năng cây sinh ra các vòng gỗ, đếm số vòng gỗ ta biết được tuồi của cây
                ",
                'page' => $i,
        ]);

        $i++;
        StudyDocument::create([
            'classroom_id' => "1",
            'content' =>
                "
                Biến dạn của thân cây:
                Một số loại cây có thân biến dạng để phù hợp với môi trường sống, ví dụ như:
                - Thân cũ, thân rễ chứa chất dự trữ.
                - Thân mọng nước chứa nước dự trữ
                - ...
                ",
                'page' => $i,
        ]);

        $i++;
        StudyDocument::create([
            'classroom_id' => "1",
            'content' =>
                "
                Lá cây
                Lá gôm có phiến và cuống.
                Phiến lá màu lục, hình bản dẹt, là phần rộng nhất của lá.
                Gân lá có 3 kiệu:
                 - hình mạng
                 - song song
                 - hình cung
                Lá có 2 nhóm chính:
                 - là đơn
                 - là kép
                ",
                'page' => $i,
        ]);

        $i++;
        StudyDocument::create([
            'classroom_id' => "1",
            'content' =>
                "
                Quang hợp lá quá trình lá cây nhờ chất diệp lục, sử dụng nước và khí CO2 và năng lượng ánh sáng mặt trời để tạo ra nước và tinh bột và khí oxi.
                ",
                'page' => $i,
        ]);

        $i++;
        StudyDocument::create([
            'classroom_id' => "1",
            'content' =>
                "
                Biến dạn của lá:
                Lá của một số loài có thể biến đổi để phù hợp với hoàn cảnh tự nhiên của chúng, ví dụ như:
                - biến thành gai
                - biến thành cuốn hoặc tay móc
                - lá dự trữ chất hữu cho
                - lá bắt mồi
                ",
                'page' => $i,
        ]);
    }
}